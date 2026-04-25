using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAll()
    {
        var categories = await context.Categories.ToListAsync();

        return Ok(categories);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Category>> GetById(Guid id)
    {
        var category = await context.Categories.FindAsync(id);

        if (category is null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> Create(Category category)
    {
        category.Id = Guid.NewGuid();
        category.CreatedAt = DateTime.UtcNow;

        context.Categories.Add(category);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, Category updatedCategory)
    {
        var category = await context.Categories.FindAsync(id);

        if (category is null)
        {
            return NotFound();
        }

        category.Name = updatedCategory.Name;
        category.Type = updatedCategory.Type;
        category.Color = updatedCategory.Color;
        category.Icon = updatedCategory.Icon;

        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var category = await context.Categories.FindAsync(id);

        if (category is null)
        {
            return NotFound();
        }

        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return NoContent();
    }
}
