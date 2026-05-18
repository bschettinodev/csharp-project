using api.Dtos.Categories;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class CategoriesController(CategoriesService categoriesService) : ApiControllerBase
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
            return HandleError(result);

        return Ok(result.Value);
    }
}
