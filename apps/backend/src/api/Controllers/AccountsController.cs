using api.Dtos.Accounts;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController(AccountsService accountsService) : ControllerBase
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
        var account = await accountsService.GetByIdAsync(id);

        if (account is null)
        {
            return NotFound();
        }

        return Ok(account);
    }

    [HttpPost]
    public async Task<ActionResult<AccountResponseDto>> Create(CreateAccountDto dto)
    {
        var account = await accountsService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateAccountDto dto)
    {
        var updated = await accountsService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await accountsService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
