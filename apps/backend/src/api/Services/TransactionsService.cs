using System.Linq.Expressions;
using api.Common;
using api.Data;
using api.Dtos.Transactions;
using api.Enums;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class TransactionsService(AppDbContext context, UsersService usersService)
{
    private static Expression<Func<Transaction, TransactionResponseDto>> TransactionProjection()
    {
        return transaction => new TransactionResponseDto
        {
            Id = transaction.Id,
            Description = transaction.Description,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Type = transaction.Type,
            AccountId = transaction.AccountId,
            AccountName = transaction.Account.Name,
            CategoryId = transaction.CategoryId,
            CategoryName = transaction.Category.Name,
            CategoryColor = transaction.Category.Color,
            CategoryIcon = transaction.Category.Icon,
            Notes = transaction.Notes,
            CreatedAt = transaction.CreatedAt,
            UpdatedAt = transaction.UpdatedAt,
        };
    }

    private async Task<decimal> CalculateCurrentBalanceAsync(Guid accountId, Guid userId)
    {
        var account = await context
            .Accounts.AsNoTracking()
            .Where(account => account.Id == accountId && account.UserId == userId)
            .Select(account => new
            {
                account.InitialBalance,

                Income = account
                    .Transactions.Where(transaction => transaction.Type == TransactionType.Income)
                    .Sum(transaction => (decimal?)transaction.Amount)
                    ?? 0,

                Expense = account
                    .Transactions.Where(transaction => transaction.Type == TransactionType.Expense)
                    .Sum(transaction => (decimal?)transaction.Amount)
                    ?? 0,
            })
            .FirstOrDefaultAsync();

        if (account is null)
            return 0;

        return account.InitialBalance + account.Income - account.Expense;
    }

    public async Task<List<TransactionResponseDto>> GetAllAsync()
    {
        var user = await usersService.GetOrCreateAsync();

        return await context
            .Transactions.AsNoTracking()
            .Where(transaction => transaction.UserId == user.Id)
            .OrderByDescending(transaction => transaction.Date)
            .Select(TransactionProjection())
            .ToListAsync();
    }

    public async Task<Result<TransactionResponseDto>> GetByIdAsync(Guid id)
    {
        var user = await usersService.GetOrCreateAsync();

        var transaction = await context
            .Transactions.AsNoTracking()
            .Where(transaction => transaction.Id == id && transaction.UserId == user.Id)
            .Select(TransactionProjection())
            .FirstOrDefaultAsync();

        if (transaction is null)
            return Result<TransactionResponseDto>.NotFound("Transaction not found.");

        return Result<TransactionResponseDto>.Success(transaction);
    }

    public async Task<Result<TransactionResponseDto>> CreateAsync(CreateTransactionDto dto)
    {
        var user = await usersService.GetOrCreateAsync();

        var account = await context
            .Accounts.AsNoTracking()
            .FirstOrDefaultAsync(account =>
                account.Id == dto.AccountId && account.UserId == user.Id
            );

        if (account is null)
            return Result<TransactionResponseDto>.NotFound("Account not found.");

        var category = await context
            .Categories.AsNoTracking()
            .FirstOrDefaultAsync(category => category.Id == dto.CategoryId);

        if (category is null)
            return Result<TransactionResponseDto>.NotFound("Category not found.");

        if (category.Type != dto.Type)
        {
            return Result<TransactionResponseDto>.BadRequest(
                "Category type must match transaction type."
            );
        }

        if (dto.Type == TransactionType.Expense)
        {
            var currentBalance = await CalculateCurrentBalanceAsync(dto.AccountId, user.Id);

            if (currentBalance < dto.Amount)
            {
                return Result<TransactionResponseDto>.BadRequest(
                    "Insufficient balance for this expense."
                );
            }
        }

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Description = dto.Description.Trim(),
            Amount = dto.Amount,
            Date = dto.Date,
            Type = dto.Type,
            AccountId = dto.AccountId,
            CategoryId = dto.CategoryId,
            Notes = dto.Notes?.Trim(),
            CreatedAt = DateTime.UtcNow,
        };

        context.Transactions.Add(transaction);

        await context.SaveChangesAsync();

        var created = await context
            .Transactions.AsNoTracking()
            .Where(transaction => transaction.Id == transaction.Id && transaction.UserId == user.Id)
            .Select(TransactionProjection())
            .FirstOrDefaultAsync();

        return Result<TransactionResponseDto>.Created(created!);
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateTransactionDto dto)
    {
        var user = await usersService.GetOrCreateAsync();

        await using var dbTransaction = await context.Database.BeginTransactionAsync();

        try
        {
            var transaction = await context.Transactions.FirstOrDefaultAsync(transaction =>
                transaction.Id == id && transaction.UserId == user.Id
            );

            if (transaction is null)
                return Result.NotFound("Transaction not found.");

            var accountExists = await context.Accounts.AnyAsync(account =>
                account.Id == dto.AccountId && account.UserId == user.Id
            );

            if (!accountExists)
                return Result.NotFound("Account not found.");

            var category = await context
                .Categories.AsNoTracking()
                .FirstOrDefaultAsync(category => category.Id == dto.CategoryId);

            if (category is null)
                return Result.NotFound("Category not found.");

            if (category.Type != dto.Type)
            {
                return Result.BadRequest("Category type must match transaction type.");
            }

            if (dto.Type == TransactionType.Expense)
            {
                var balanceWithoutCurrent = await CalculateCurrentBalanceAsync(
                    dto.AccountId,
                    user.Id
                );

                var adjustment =
                    transaction.AccountId == dto.AccountId
                        ? (
                            transaction.Type == TransactionType.Income
                                ? -transaction.Amount
                                : transaction.Amount
                        )
                        : 0;

                var availableBalance = balanceWithoutCurrent + adjustment;

                if (availableBalance < dto.Amount)
                {
                    return Result.BadRequest("Insufficient balance for this expense.");
                }
            }

            transaction.Description = dto.Description.Trim();
            transaction.Amount = dto.Amount;
            transaction.Date = dto.Date;
            transaction.Type = dto.Type;
            transaction.AccountId = dto.AccountId;
            transaction.CategoryId = dto.CategoryId;
            transaction.Notes = dto.Notes?.Trim();
            transaction.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            await dbTransaction.CommitAsync();

            return Result.Success();
        }
        catch
        {
            await dbTransaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var user = await usersService.GetOrCreateAsync();

        var transaction = await context.Transactions.FirstOrDefaultAsync(transaction =>
            transaction.Id == id && transaction.UserId == user.Id
        );

        if (transaction is null)
            return Result.NotFound("Transaction not found.");

        context.Transactions.Remove(transaction);

        await context.SaveChangesAsync();

        return Result.Success();
    }
}
