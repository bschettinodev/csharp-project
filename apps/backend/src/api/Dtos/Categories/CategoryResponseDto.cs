using api.Enums;

namespace api.Dtos.Categories;

public class CategoryResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TransactionType Type { get; set; }
    public string Color { get; set; } = "#FFFFFF";
    public string? Icon { get; set; }
}
