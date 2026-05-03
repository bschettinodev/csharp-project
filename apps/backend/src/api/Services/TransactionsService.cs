using api.Common;
using api.Data;
using api.Dtos.Transactions;
using api.Enums;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class TransactionsService(AppDbContext context)
{
    public async Task<List<TransactionResponseDto>> GetAllAsync()
    {
        var transactions = await context
            .Transactions.Include(transaction => transaction.Account)
            .Include(transaction => transaction.Category)
            .OrderByDescending(transaction => transaction.Date)
            .ToListAsync();

        return transactions.Select(ToResponseDto).ToList();
    }

    public async Task<Result<TransactionResponseDto>> GetByIdAsync(Guid id)
    {
        var transaction = await context
            .Transactions.Include(transaction => transaction.Account)
            .Include(transaction => transaction.Category)
            .FirstOrDefaultAsync(transaction => transaction.Id == id);

        if (transaction is null)
        {
            return Result<TransactionResponseDto>.NotFound("Transaction not found.");
        }

        return Result<TransactionResponseDto>.Success(ToResponseDto(transaction));
    }

    public async Task<Result<TransactionResponseDto>> CreateAsync(CreateTransactionDto dto)
    {
        var account = await context.Accounts.FindAsync(dto.AccountId);

        if (account is null)
        {
            return Result<TransactionResponseDto>.NotFound("Account not found.");
        }

        var category = await context.Categories.FindAsync(dto.CategoryId);

        if (category is null)
        {
            return Result<TransactionResponseDto>.NotFound("Category not found.");
        }

        if (category.Type != dto.Type)
        {
            return Result<TransactionResponseDto>.BadRequest(
                "Category type must match transaction type."
            );
        }

        if (dto.Type == TransactionType.Expense && account.CurrentBalance < dto.Amount)
        {
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

        account.ApplyTransaction(transaction.Type, transaction.Amount);

        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        transaction.Account = account;
        transaction.Category = category;

        return Result<TransactionResponseDto>.Created(ToResponseDto(transaction));
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateTransactionDto dto)
    {
        var transaction = await context
            .Transactions.Include(transaction => transaction.Account)
            .FirstOrDefaultAsync(transaction => transaction.Id == id);

        if (transaction is null)
        {
            return Result.NotFound("Transaction not found.");
        }

        var newAccount = await context.Accounts.FindAsync(dto.AccountId);

        if (newAccount is null)
        {
            return Result.NotFound("Account not found.");
        }

        var newCategory = await context.Categories.FindAsync(dto.CategoryId);

        if (newCategory is null)
        {
            return Result.NotFound("Category not found.");
        }

        if (newCategory.Type != dto.Type)
        {
            return Result.BadRequest("Category type must match transaction type.");
        }

        transaction.Account.RevertTransaction(transaction.Type, transaction.Amount);

        if (dto.Type == TransactionType.Expense && newAccount.CurrentBalance < dto.Amount)
        {
            transaction.Account.ApplyTransaction(transaction.Type, transaction.Amount);

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

        newAccount.ApplyTransaction(transaction.Type, transaction.Amount);

        await context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var transaction = await context
            .Transactions.Include(transaction => transaction.Account)
            .FirstOrDefaultAsync(transaction => transaction.Id == id);

        if (transaction is null)
        {
            return Result.NotFound("Transaction not found.");
        }

        transaction.Account.RevertTransaction(transaction.Type, transaction.Amount);

        context.Transactions.Remove(transaction);
        await context.SaveChangesAsync();

        return Result.Success();
    }

    private static TransactionResponseDto ToResponseDto(Transaction transaction)
    {
        return new TransactionResponseDto
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
}
