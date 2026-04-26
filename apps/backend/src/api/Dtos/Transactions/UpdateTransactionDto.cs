using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Dtos.Transactions;

public class UpdateTransactionDto
{
    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [EnumDataType(typeof(TransactionType))]
    public TransactionType Type { get; set; }

    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }
}
