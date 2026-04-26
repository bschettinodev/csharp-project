using api.Common;
using api.Data;
using api.Dtos.Accounts;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class AccountsService(AppDbContext context)
{
    public async Task<List<AccountResponseDto>> GetAllAsync()
    {
        var accounts = await context.Accounts.OrderBy(account => account.Name).ToListAsync();

        return accounts.Select(ToResponseDto).ToList();
    }

    public async Task<Result<AccountResponseDto>> GetByIdAsync(Guid id)
    {
        var account = await context.Accounts.FindAsync(id);

        if (account is null)
        {
            return Result<AccountResponseDto>.NotFound("Account not found.");
        }

        return Result<AccountResponseDto>.Success(ToResponseDto(account));
    }

    public async Task<Result<AccountResponseDto>> CreateAsync(CreateAccountDto dto)
    {
        var name = dto.Name.Trim();

        var nameAlreadyExists = await context.Accounts.AnyAsync(account =>
            account.Name.ToLower() == name.ToLower()
        );

        if (nameAlreadyExists)
        {
            return Result<AccountResponseDto>.Conflict("An account with this name already exists.");
        }

        var account = new Account
        {
            Id = Guid.NewGuid(),
            Name = name,
            Type = dto.Type,
            InitialBalance = dto.InitialBalance,
            CurrentBalance = dto.InitialBalance,
            CreatedAt = DateTime.UtcNow,
        };

        context.Accounts.Add(account);
        await context.SaveChangesAsync();

        return Result<AccountResponseDto>.Created(ToResponseDto(account));
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateAccountDto dto)
    {
        var account = await context.Accounts.FindAsync(id);

        if (account is null)
        {
            return Result.NotFound("Account not found.");
        }

        var name = dto.Name.Trim();

        var nameAlreadyExists = await context.Accounts.AnyAsync(existingAccount =>
            existingAccount.Id != id && existingAccount.Name.ToLower() == name.ToLower()
        );

        if (nameAlreadyExists)
        {
            return Result.Conflict("An account with this name already exists.");
        }

        var hasTransactions = await context.Transactions.AnyAsync(transaction =>
            transaction.AccountId == id
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
            account.CurrentBalance = dto.InitialBalance;
        }

        await context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var account = await context.Accounts.FindAsync(id);

        if (account is null)
        {
            return Result.NotFound("Account not found.");
        }

        var hasTransactions = await context.Transactions.AnyAsync(transaction =>
            transaction.AccountId == id
        );

        if (hasTransactions)
        {
            return Result.Conflict("Account cannot be deleted because it has transactions.");
        }

        context.Accounts.Remove(account);
        await context.SaveChangesAsync();

        return Result.Success();
    }

    private static AccountResponseDto ToResponseDto(Account account)
    {
        return new AccountResponseDto
        {
            Id = account.Id,
            Name = account.Name,
            Type = account.Type,
            InitialBalance = account.InitialBalance,
            CurrentBalance = account.CurrentBalance,
            CreatedAt = account.CreatedAt,
        };
    }
}
