using System.Linq.Expressions;
using api.Common;
using api.Data;
using api.Dtos.Accounts;
using api.Enums;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class AccountsService(AppDbContext context, UsersService usersService)
{
    private static Expression<Func<Account, AccountResponseDto>> AccountProjection()
    {
        return account => new AccountResponseDto
        {
            Id = account.Id,
            Name = account.Name,
            Type = account.Type,
            InitialBalance = account.InitialBalance,

            CurrentBalance =
                account.InitialBalance
                + (
                    account
                        .Transactions.Where(transaction =>
                            transaction.Type == TransactionType.Income
                        )
                        .Sum(transaction => (decimal?)transaction.Amount)
                    ?? 0
                )
                - (
                    account
                        .Transactions.Where(transaction =>
                            transaction.Type == TransactionType.Expense
                        )
                        .Sum(transaction => (decimal?)transaction.Amount)
                    ?? 0
                ),

            CreatedAt = account.CreatedAt,
        };
    }

    public async Task<List<AccountResponseDto>> GetAllAsync()
    {
        var user = await usersService.GetOrCreateAsync();

        return await context
            .Accounts.AsNoTracking()
            .Where(account => account.UserId == user.Id)
            .OrderBy(account => account.Name)
            .Select(AccountProjection())
            .ToListAsync();
    }

    public async Task<Result<AccountResponseDto>> GetByIdAsync(Guid id)
    {
        var user = await usersService.GetOrCreateAsync();

        var account = await context
            .Accounts.AsNoTracking()
            .Where(account => account.Id == id && account.UserId == user.Id)
            .Select(AccountProjection())
            .FirstOrDefaultAsync();

        if (account is null)
        {
            return Result<AccountResponseDto>.NotFound("Account not found.");
        }

        return Result<AccountResponseDto>.Success(account);
    }

    public async Task<Result<AccountResponseDto>> CreateAsync(CreateAccountDto dto)
    {
        var user = await usersService.GetOrCreateAsync();

        var name = dto.Name.Trim();

        var nameAlreadyExists = await context.Accounts.AnyAsync(account =>
            account.UserId == user.Id && EF.Functions.ILike(account.Name, name)
        );

        if (nameAlreadyExists)
        {
            return Result<AccountResponseDto>.Conflict("An account with this name already exists.");
        }

        var account = new Account
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Name = name,
            Type = dto.Type,
            InitialBalance = dto.InitialBalance,
            CreatedAt = DateTime.UtcNow,
        };

        context.Accounts.Add(account);

        await context.SaveChangesAsync();

        return Result<AccountResponseDto>.Created(
            new AccountResponseDto
            {
                Id = account.Id,
                Name = account.Name,
                Type = account.Type,
                InitialBalance = account.InitialBalance,
                CurrentBalance = account.InitialBalance,
                CreatedAt = account.CreatedAt,
            }
        );
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateAccountDto dto)
    {
        var user = await usersService.GetOrCreateAsync();

        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var account = await context.Accounts.FirstOrDefaultAsync(account =>
                account.Id == id && account.UserId == user.Id
            );

            if (account is null)
            {
                return Result.NotFound("Account not found.");
            }

            var name = dto.Name.Trim();

            var nameAlreadyExists = await context.Accounts.AnyAsync(existing =>
                existing.Id != id
                && existing.UserId == user.Id
                && EF.Functions.ILike(existing.Name, name)
            );

            if (nameAlreadyExists)
            {
                return Result.Conflict("An account with this name already exists.");
            }

            var hasTransactions = await context.Transactions.AnyAsync(transaction =>
                transaction.AccountId == id && transaction.UserId == user.Id
            );

            if (hasTransactions && account.InitialBalance != dto.InitialBalance)
            {
                return Result.Conflict(
                    "Initial balance cannot be changed because this account already has transactions."
                );
            }

            account.Name = name;
            account.Type = dto.Type;

            if (!hasTransactions)
            {
                account.InitialBalance = dto.InitialBalance;
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Result.Success();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var user = await usersService.GetOrCreateAsync();

        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var account = await context.Accounts.FirstOrDefaultAsync(account =>
                account.Id == id && account.UserId == user.Id
            );

            if (account is null)
            {
                return Result.NotFound("Account not found.");
            }

            var hasTransactions = await context.Transactions.AnyAsync(existingTransaction =>
                existingTransaction.AccountId == id && existingTransaction.UserId == user.Id
            );

            if (hasTransactions)
            {
                return Result.Conflict(
                    "This account cannot be deleted because it is used in transactions."
                );
            }

            context.Accounts.Remove(account);

            await context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Result.Success();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
