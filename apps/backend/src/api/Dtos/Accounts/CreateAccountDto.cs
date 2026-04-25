using api.Enums;

namespace api.Dtos.Accounts;

public class CreateAccountDto
{
    public string Name { get; set; } = string.Empty;
    public AccountType Type { get; set; }
    public decimal InitialBalance { get; set; }
}
