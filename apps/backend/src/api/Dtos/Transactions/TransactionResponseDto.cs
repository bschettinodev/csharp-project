using api.Enums;

namespace api.Dtos.Transactions;

public class TransactionResponseDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }

    public Guid AccountId { get; set; }
    public string AccountName { get; set; } = string.Empty;

    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;

    public string CategoryColor { get; set; } = string.Empty;
    public string? CategoryIcon { get; set; }

    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
