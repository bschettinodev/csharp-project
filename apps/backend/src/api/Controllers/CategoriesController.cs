using api.Dtos.Categories;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(CategoriesService categoriesService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetAll()
    {
        var categories = await categoriesService.GetAllAsync();

        return Ok(categories);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryResponseDto>> GetById(Guid id)
    {
        var result = await categoriesService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, new { message = result.Error });
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryResponseDto>> Create(CreateCategoryDto dto)
    {
        var result = await categoriesService.CreateAsync(dto);

        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, new { message = result.Error });
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryDto dto)
    {
        var result = await categoriesService.UpdateAsync(id, dto);

        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, new { message = result.Error });
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await categoriesService.DeleteAsync(id);

        if (!result.IsSuccess)
        {
            return StatusCode(result.StatusCode, new { message = result.Error });
        }

        return NoContent();
    }
}
