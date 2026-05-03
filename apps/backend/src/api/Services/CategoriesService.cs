using api.Common;
using api.Data;
using api.Dtos.Categories;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class CategoriesService(AppDbContext context)
{
    public async Task<List<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await context
            .Categories.OrderBy(category => category.Type)
            .ThenBy(category => category.Name)
            .ToListAsync();

        return categories.Select(ToResponseDto).ToList();
    }

    public async Task<Result<CategoryResponseDto>> GetByIdAsync(Guid id)
    {
        var category = await context.Categories.FindAsync(id);

        if (category is null)
        {
            return Result<CategoryResponseDto>.NotFound("Category not found.");
        }

        return Result<CategoryResponseDto>.Success(ToResponseDto(category));
    }

    private static CategoryResponseDto ToResponseDto(Category category)
    {
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Type = category.Type,
            Color = category.Color,
            Icon = category.Icon,
        };
    }
}
