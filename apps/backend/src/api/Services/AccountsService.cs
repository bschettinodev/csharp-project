using api.Data;
using api.Dtos.Accounts;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class AccountsService(AppDbContext context)
{
    public async Task<List<AccountResponseDto>> GetAllAsync()
    {
        var accounts = await context.Accounts.ToListAsync();

        return accounts
            .Select(account => new AccountResponseDto
            {
                Id = account.Id,
                Name = account.Name,
                Type = account.Type,
                InitialBalance = account.InitialBalance,
                CurrentBalance = account.CurrentBalance,
                CreatedAt = account.CreatedAt,
            })
            .ToList();
    }

    public async Task<AccountResponseDto?> GetByIdAsync(Guid id)
    {
        var account = await context.Accounts.FindAsync(id);

        if (account is null)
            return null;

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

    public async Task<AccountResponseDto> CreateAsync(CreateAccountDto dto)
    {
        var account = new Account
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Type = dto.Type,
            InitialBalance = dto.InitialBalance,
            CurrentBalance = dto.InitialBalance,
            CreatedAt = DateTime.UtcNow,
        };

        context.Accounts.Add(account);
        await context.SaveChangesAsync();

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

    public async Task<bool> UpdateAsync(Guid id, UpdateAccountDto dto)
    {
        var account = await context.Accounts.FindAsync(id);

        if (account is null)
            return false;

        account.Name = dto.Name;
        account.Type = dto.Type;
        account.InitialBalance = dto.InitialBalance;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var account = await context.Accounts.FindAsync(id);

        if (account is null)
            return false;

        context.Accounts.Remove(account);
        await context.SaveChangesAsync();

        return true;
    }
}
