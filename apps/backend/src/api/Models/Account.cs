using api.Enums;

namespace api.Models;

public class Account
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public AccountType Type { get; set; }

    public decimal InitialBalance { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public ICollection<Transaction> Transactions { get; set; } = [];
}
