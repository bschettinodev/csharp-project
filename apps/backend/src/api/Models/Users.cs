namespace api.Models;

public class User
{
    public Guid Id { get; set; }

    public string FirebaseUid { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public bool EmailVerified { get; set; }

    public string? DisplayName { get; set; }

    public string? PhotoUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public ICollection<Account> Accounts { get; set; } = [];

    public ICollection<Transaction> Transactions { get; set; } = [];
}
