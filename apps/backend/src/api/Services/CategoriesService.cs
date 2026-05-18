using System.Linq.Expressions;
using api.Common;
using api.Data;
using api.Dtos.Categories;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class CategoriesService(AppDbContext context)
{
    private static Expression<Func<Category, CategoryResponseDto>> CategoryProjection()
    {
        return category => new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Type = category.Type,
            Color = category.Color,
            Icon = category.Icon,
        };
    }

    public async Task<List<CategoryResponseDto>> GetAllAsync()
    {
        return await context
            .Categories.AsNoTracking()
            .OrderBy(category => category.Type)
            .ThenBy(category => category.Name)
            .Select(CategoryProjection())
            .ToListAsync();
    }

    public async Task<Result<CategoryResponseDto>> GetByIdAsync(Guid id)
    {
        var category = await context
            .Categories.AsNoTracking()
            .Where(category => category.Id == id)
            .Select(CategoryProjection())
            .FirstOrDefaultAsync();

        if (category is null)
            return Result<CategoryResponseDto>.NotFound("Category not found.");

        return Result<CategoryResponseDto>.Success(category);
    }
}
