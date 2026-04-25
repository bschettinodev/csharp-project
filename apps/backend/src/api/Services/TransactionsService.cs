using api.Data;
using api.Dtos.Transactions;
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

    public async Task<TransactionResponseDto?> GetByIdAsync(Guid id)
    {
        var transaction = await context
            .Transactions.Include(transaction => transaction.Account)
            .Include(transaction => transaction.Category)
            .FirstOrDefaultAsync(transaction => transaction.Id == id);

        if (transaction is null)
        {
            return null;
        }

        return ToResponseDto(transaction);
    }

    public async Task<TransactionResponseDto?> CreateAsync(CreateTransactionDto dto)
    {
        var account = await context.Accounts.FindAsync(dto.AccountId);

        if (account is null)
        {
            return null;
        }

        var category = await context.Categories.FindAsync(dto.CategoryId);

        if (category is null)
        {
            return null;
        }

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Description = dto.Description,
            Amount = dto.Amount,
            Date = dto.Date,
            Type = dto.Type,
            AccountId = dto.AccountId,
            CategoryId = dto.CategoryId,
            Notes = dto.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        account.ApplyTransaction(transaction.Type, transaction.Amount);

        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        transaction.Account = account;
        transaction.Category = category;

        return ToResponseDto(transaction);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateTransactionDto dto)
    {
        var transaction = await context
            .Transactions.Include(transaction => transaction.Account)
            .FirstOrDefaultAsync(transaction => transaction.Id == id);

        if (transaction is null)
        {
            return false;
        }

        var newAccount = await context.Accounts.FindAsync(dto.AccountId);

        if (newAccount is null)
        {
            return false;
        }

        var categoryExists = await context.Categories.AnyAsync(category =>
            category.Id == dto.CategoryId
        );

        if (!categoryExists)
        {
            return false;
        }

        transaction.Account.RevertTransaction(transaction.Type, transaction.Amount);

        transaction.Description = dto.Description;
        transaction.Amount = dto.Amount;
        transaction.Date = dto.Date;
        transaction.Type = dto.Type;
        transaction.AccountId = dto.AccountId;
        transaction.CategoryId = dto.CategoryId;
        transaction.Notes = dto.Notes;
        transaction.UpdatedAt = DateTime.UtcNow;

        newAccount.ApplyTransaction(transaction.Type, transaction.Amount);

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var transaction = await context
            .Transactions.Include(transaction => transaction.Account)
            .FirstOrDefaultAsync(transaction => transaction.Id == id);

        if (transaction is null)
        {
            return false;
        }

        transaction.Account.RevertTransaction(transaction.Type, transaction.Amount);

        context.Transactions.Remove(transaction);
        await context.SaveChangesAsync();

        return true;
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
            Notes = transaction.Notes,
            CreatedAt = transaction.CreatedAt,
            UpdatedAt = transaction.UpdatedAt,
        };
    }
}
