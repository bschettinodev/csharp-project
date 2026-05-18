using System.Linq.Expressions;
using api.Common;
using api.Data;
using api.Dtos.Transactions;
using api.Enums;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class TransactionsService(AppDbContext context)
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

    private static Expression<Func<Transaction, bool>> ByIdFilter(Guid id)
    {
        return transaction => transaction.Id == id;
    }

    private async Task<decimal> CalculateCurrentBalanceAsync(Guid accountId)
    {
        var account = await context
            .Accounts.AsNoTracking()
            .Where(a => a.Id == accountId)
            .Select(a => new
            {
                a.InitialBalance,
                Income = a.Transactions.Where(t => t.Type == TransactionType.Income)
                    .Sum(t => (decimal?)t.Amount)
                    ?? 0,
                Expense = a.Transactions.Where(t => t.Type == TransactionType.Expense)
                    .Sum(t => (decimal?)t.Amount)
                    ?? 0,
            })
            .FirstOrDefaultAsync();

        if (account is null)
            return 0;

        return account.InitialBalance + account.Income - account.Expense;
    }

    public async Task<List<TransactionResponseDto>> GetAllAsync()
    {
        return await context
            .Transactions.AsNoTracking()
            .OrderByDescending(transaction => transaction.Date)
            .Select(TransactionProjection())
            .ToListAsync();
    }

    public async Task<Result<TransactionResponseDto>> GetByIdAsync(Guid id)
    {
        var transaction = await context
            .Transactions.AsNoTracking()
            .Where(ByIdFilter(id))
            .Select(TransactionProjection())
            .FirstOrDefaultAsync();

        if (transaction is null)
            return Result<TransactionResponseDto>.NotFound("Transaction not found.");

        return Result<TransactionResponseDto>.Success(transaction);
    }

    public async Task<Result<TransactionResponseDto>> CreateAsync(CreateTransactionDto dto)
    {
        var account = await context
            .Accounts.AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == dto.AccountId);

        if (account is null)
            return Result<TransactionResponseDto>.NotFound("Account not found.");

        var category = await context
            .Categories.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == dto.CategoryId);

        if (category is null)
            return Result<TransactionResponseDto>.NotFound("Category not found.");

        if (category.Type != dto.Type)
            return Result<TransactionResponseDto>.BadRequest(
                "Category type must match transaction type."
            );

        if (dto.Type == TransactionType.Expense)
        {
            var currentBalance = await CalculateCurrentBalanceAsync(dto.AccountId);

            if (currentBalance < dto.Amount)
                return Result<TransactionResponseDto>.BadRequest(
                    "Insufficient balance for this expense."
                );
        }

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
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
            .Where(ByIdFilter(transaction.Id))
            .Select(TransactionProjection())
            .FirstOrDefaultAsync();

        return Result<TransactionResponseDto>.Created(created!);
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateTransactionDto dto)
    {
        await using var dbTransaction = await context.Database.BeginTransactionAsync();

        try
        {
            var transaction = await context.Transactions.FirstOrDefaultAsync(ByIdFilter(id));

            if (transaction is null)
                return Result.NotFound("Transaction not found.");

            var accountExists = await context.Accounts.AnyAsync(a => a.Id == dto.AccountId);

            if (!accountExists)
                return Result.NotFound("Account not found.");

            var category = await context
                .Categories.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == dto.CategoryId);

            if (category is null)
                return Result.NotFound("Category not found.");

            if (category.Type != dto.Type)
                return Result.BadRequest("Category type must match transaction type.");

            if (dto.Type == TransactionType.Expense)
            {
                var balanceWithoutCurrent = await CalculateCurrentBalanceAsync(dto.AccountId);

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
                    return Result.BadRequest("Insufficient balance for this expense.");
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
        var transaction = await context.Transactions.FirstOrDefaultAsync(ByIdFilter(id));

        if (transaction is null)
            return Result.NotFound("Transaction not found.");

        context.Transactions.Remove(transaction);
        await context.SaveChangesAsync();

        return Result.Success();
    }
}
