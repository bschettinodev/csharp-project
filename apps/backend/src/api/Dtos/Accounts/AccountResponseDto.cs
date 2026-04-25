using api.Enums;

namespace api.Dtos.Accounts;

public class AccountResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public AccountType Type { get; set; }
    public decimal InitialBalance { get; set; }
    public decimal CurrentBalance { get; set; }
    public DateTime CreatedAt { get; set; }
}
