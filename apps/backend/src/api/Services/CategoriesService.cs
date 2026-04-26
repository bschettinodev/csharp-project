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
        var categories = await context.Categories.OrderBy(category => category.Name).ToListAsync();

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

    public async Task<Result<CategoryResponseDto>> CreateAsync(CreateCategoryDto dto)
    {
        var name = dto.Name.Trim();

        var categoryAlreadyExists = await context.Categories.AnyAsync(category =>
            category.Name.ToLower() == name.ToLower() && category.Type == dto.Type
        );

        if (categoryAlreadyExists)
        {
            return Result<CategoryResponseDto>.Conflict(
                "A category with this name and type already exists."
            );
        }

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = name,
            Type = dto.Type,
            Color = dto.Color,
            Icon = dto.Icon,
            CreatedAt = DateTime.UtcNow,
        };

        context.Categories.Add(category);
        await context.SaveChangesAsync();

        return Result<CategoryResponseDto>.Created(ToResponseDto(category));
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateCategoryDto dto)
    {
        var category = await context.Categories.FindAsync(id);

        if (category is null)
        {
            return Result.NotFound("Category not found.");
        }

        var name = dto.Name.Trim();

        var categoryAlreadyExists = await context.Categories.AnyAsync(existingCategory =>
            existingCategory.Id != id
            && existingCategory.Name.ToLower() == name.ToLower()
            && existingCategory.Type == dto.Type
        );

        if (categoryAlreadyExists)
        {
            return Result.Conflict("A category with this name and type already exists.");
        }

        var hasTransactions = await context.Transactions.AnyAsync(transaction =>
            transaction.CategoryId == id
        );

        if (hasTransactions && category.Type != dto.Type)
        {
            return Result.Conflict(
                "Category type cannot be changed because this category already has transactions."
            );
        }

        category.Name = name;
        category.Color = dto.Color;
        category.Icon = dto.Icon;

        if (!hasTransactions)
        {
            category.Type = dto.Type;
        }

        await context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var category = await context.Categories.FindAsync(id);

        if (category is null)
        {
            return Result.NotFound("Category not found.");
        }

        var hasTransactions = await context.Transactions.AnyAsync(transaction =>
            transaction.CategoryId == id
        );

        if (hasTransactions)
        {
            return Result.Conflict("Category cannot be deleted because it has transactions.");
        }

        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return Result.Success();
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
            CreatedAt = category.CreatedAt,
        };
    }
}
