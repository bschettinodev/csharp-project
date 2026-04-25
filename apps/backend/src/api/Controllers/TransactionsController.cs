using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetAll()
    {
        var transactions = await context
            .Transactions.Include(transaction => transaction.Account)
            .Include(transaction => transaction.Category)
            .OrderByDescending(transaction => transaction.Date)
            .ToListAsync();

        return Ok(transactions);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Transaction>> GetById(Guid id)
    {
        var transaction = await context
            .Transactions.Include(transaction => transaction.Account)
            .Include(transaction => transaction.Category)
            .FirstOrDefaultAsync(transaction => transaction.Id == id);

        if (transaction is null)
        {
            return NotFound();
        }

        return Ok(transaction);
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> Create(Transaction transaction)
    {
        var accountExists = await context.Accounts.AnyAsync(account =>
            account.Id == transaction.AccountId
        );
        var categoryExists = await context.Categories.AnyAsync(category =>
            category.Id == transaction.CategoryId
        );

        if (!accountExists)
        {
            return BadRequest("Account not found.");
        }

        if (!categoryExists)
        {
            return BadRequest("Category not found.");
        }

        transaction.Id = Guid.NewGuid();
        transaction.CreatedAt = DateTime.UtcNow;

        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, Transaction updatedTransaction)
    {
        var transaction = await context.Transactions.FindAsync(id);

        if (transaction is null)
        {
            return NotFound();
        }

        var accountExists = await context.Accounts.AnyAsync(account =>
            account.Id == updatedTransaction.AccountId
        );
        var categoryExists = await context.Categories.AnyAsync(category =>
            category.Id == updatedTransaction.CategoryId
        );

        if (!accountExists)
        {
            return BadRequest("Account not found.");
        }

        if (!categoryExists)
        {
            return BadRequest("Category not found.");
        }

        transaction.Description = updatedTransaction.Description;
        transaction.Amount = updatedTransaction.Amount;
        transaction.Date = updatedTransaction.Date;
        transaction.Type = updatedTransaction.Type;
        transaction.AccountId = updatedTransaction.AccountId;
        transaction.CategoryId = updatedTransaction.CategoryId;
        transaction.Notes = updatedTransaction.Notes;
        transaction.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var transaction = await context.Transactions.FindAsync(id);

        if (transaction is null)
        {
            return NotFound();
        }

        context.Transactions.Remove(transaction);
        await context.SaveChangesAsync();

        return NoContent();
    }
}
