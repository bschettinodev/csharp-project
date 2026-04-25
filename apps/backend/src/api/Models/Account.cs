using api.Enums;

namespace api.Models;

public class Account
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public AccountType Type { get; set; }
    public decimal InitialBalance { get; set; }
    public decimal CurrentBalance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public void ApplyTransaction(TransactionType type, decimal amount)
    {
        if (type == TransactionType.Income)
        {
            CurrentBalance += amount;
            return;
        }

        CurrentBalance -= amount;
    }

    public void RevertTransaction(TransactionType type, decimal amount)
    {
        if (type == TransactionType.Income)
        {
            CurrentBalance -= amount;
            return;
        }

        CurrentBalance += amount;
    }
}
