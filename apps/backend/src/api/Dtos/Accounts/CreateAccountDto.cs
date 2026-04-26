using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Dtos.Accounts;

public class CreateAccountDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [EnumDataType(typeof(AccountType))]
    public AccountType Type { get; set; }

    [Range(
        0,
        double.MaxValue,
        ErrorMessage = "Initial balance must be greater than or equal to zero."
    )]
    public decimal InitialBalance { get; set; }
}
