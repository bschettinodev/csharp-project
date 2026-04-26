using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Dtos.Categories;

public class CreateCategoryDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [EnumDataType(typeof(TransactionType))]
    public TransactionType Type { get; set; }

    [Required]
    [RegularExpression("^#([A-Fa-f0-9]{6})$", ErrorMessage = "Invalid hex color.")]
    public string Color { get; set; } = "#FFFFFF";

    [StringLength(50)]
    public string? Icon { get; set; }
}
