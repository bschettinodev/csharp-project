using api.Dtos.Accounts;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class AccountsController(AccountsService accountsService) : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountResponseDto>>> GetAll()
    {
        var accounts = await accountsService.GetAllAsync();
        return Ok(accounts);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AccountResponseDto>> GetById(Guid id)
    {
        var result = await accountsService.GetByIdAsync(id);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult<AccountResponseDto>> Create(CreateAccountDto dto)
    {
        var result = await accountsService.CreateAsync(dto);

        if (!result.IsSuccess)
            return HandleError(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateAccountDto dto)
    {
        var result = await accountsService.UpdateAsync(id, dto);

        if (!result.IsSuccess)
            return HandleError(result);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await accountsService.DeleteAsync(id);

        if (!result.IsSuccess)
            return HandleError(result);

        return NoContent();
    }
}
