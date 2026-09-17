using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerAPI.DTOs.Expenses;
using ExpenseTrackerAPI.Services;

namespace ExpenseTrackerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _service;

    public ExpensesController(IExpenseService service)
    {
        _service = service;
    }

    //// GET: api/expenses
    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAll([FromQuery] int? userId)
    //{
    //    var expenses = userId.HasValue
    //        ? await _service.GetByUserIdAsync(userId.Value)
    //        : await _service.GetAllAsync();

    //    return Ok(expenses);
    //}

    // GET: api/expenses?userId=1&categoryId=2&fromDate=2026-09-01&toDate=2026-09-30&search=food
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAll([FromQuery] ExpenseFilterDto filter)
    {
        var expenses = await _service.GetFilteredAsync(filter);
        return Ok(expenses);
    }

    // GET: api/expenses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseDto>> GetById(int id)
    {
        var expense = await _service.GetByIdAsync(id);
        if (expense is null)
            return NotFound(new { message = $"Expense with id {id} not found." });

        return Ok(expense);
    }

    // POST: api/expenses
    [HttpPost]
    public async Task<ActionResult<ExpenseDto>> Create([FromBody] CreateExpenseDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/expenses/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateExpenseDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, dto);
        if (!updated)
            return NotFound(new { message = $"Expense with id {id} not found." });

        return NoContent();
    }

    // DELETE: api/expenses/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Expense with id {id} not found." });

        return NoContent();
    }
}