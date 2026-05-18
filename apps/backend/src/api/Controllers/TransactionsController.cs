using api.Dtos.Transactions;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class TransactionsController(TransactionsService transactionsService) : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionResponseDto>>> GetAll()
    {
        var transactions = await transactionsService.GetAllAsync();
        return Ok(transactions);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TransactionResponseDto>> GetById(Guid id)
    {
        var result = await transactionsService.GetByIdAsync(id);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult<TransactionResponseDto>> Create(CreateTransactionDto dto)
    {
        var result = await transactionsService.CreateAsync(dto);

        if (!result.IsSuccess)
            return HandleError(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateTransactionDto dto)
    {
        var result = await transactionsService.UpdateAsync(id, dto);

        if (!result.IsSuccess)
            return HandleError(result);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await transactionsService.DeleteAsync(id);

        if (!result.IsSuccess)
            return HandleError(result);

        return NoContent();
    }
}
