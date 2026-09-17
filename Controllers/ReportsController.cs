using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerAPI.DTOs.Expenses;
using ExpenseTrackerAPI.DTOs.Reports;
using ExpenseTrackerAPI.Services;

namespace ExpenseTrackerAPI.Controllers;

[ApiController]
[Route("api/reports")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly IReportService _service;

    public ReportsController(IReportService service)
    {
        _service = service;
    }

    // GET: api/reports/monthly?userId=1&year=2026&month=9
    [HttpGet("monthly")]
    public async Task<ActionResult<MonthlyReportDto>> GetMonthly(
        [FromQuery] int userId, [FromQuery] int year, [FromQuery] int month)
    {
        var report = await _service.GetMonthlyReportAsync(userId, year, month);
        return Ok(report);
    }

    // GET: api/reports/category-summary?userId=1&fromDate=...&toDate=...&categoryId=...
    [HttpGet("category-summary")]
    public async Task<ActionResult<CategorySummaryDto>> GetCategorySummary(
        [FromQuery] ExpenseFilterDto filter)
    {
        var summary = await _service.GetCategorySummaryAsync(filter);
        return Ok(summary);
    }
}