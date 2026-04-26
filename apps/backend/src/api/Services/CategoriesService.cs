using api.Data;
using api.Dtos.Categories;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class CategoriesService(AppDbContext context)
{
    public async Task<List<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await context.Categories.ToListAsync();

        return categories
            .Select(category => new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Type = category.Type,
                Color = category.Color,
                Icon = category.Icon,
                CreatedAt = category.CreatedAt,
            })
            .ToList();
    }

    public async Task<CategoryResponseDto?> GetByIdAsync(Guid id)
    {
        var category = await context.Categories.FindAsync(id);

        if (category is null)
            return null;

        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Type = category.Type,
            Color = category.Color,
            Icon = category.Icon,
            CreatedAt = category.CreatedAt,
        };
    }

    public async Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Type = dto.Type,
            Color = dto.Color,
            Icon = dto.Icon,
            CreatedAt = DateTime.UtcNow,
        };

        context.Categories.Add(category);
        await context.SaveChangesAsync();

        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Type = category.Type,
            Color = category.Color,
            Icon = category.Icon,
            CreatedAt = category.CreatedAt,
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateCategoryDto dto)
    {
        var category = await context.Categories.FindAsync(id);

        if (category is null)
        {
            return false;
        }

        category.Name = dto.Name;
        category.Type = dto.Type;
        category.Color = dto.Color;
        category.Icon = dto.Icon;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var category = await context.Categories.FindAsync(id);

        if (category is null)
        {
            return false;
        }

        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return true;
    }
}
