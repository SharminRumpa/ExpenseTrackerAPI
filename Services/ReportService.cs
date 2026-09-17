using ExpenseTrackerAPI.DTOs.Expenses;
using ExpenseTrackerAPI.DTOs.Reports;
using ExpenseTrackerAPI.Repositories;

namespace ExpenseTrackerAPI.Services;

public class ReportService : IReportService
{
    private readonly IExpenseRepository _expenseRepository;

    public ReportService(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<MonthlyReportDto> GetMonthlyReportAsync(int userId, int year, int month)
    {
        if (month < 1 || month > 12)
            throw new ArgumentException("Month must be between 1 and 12.");

        var fromDate = new DateTime(year, month, 1);
        var toDate = fromDate.AddMonths(1).AddDays(-1);

        var filter = new ExpenseFilterDto
        {
            UserId = userId,
            FromDate = fromDate,
            ToDate = toDate
        };

        var expenses = await _expenseRepository.GetFilteredAsync(filter);
        var expenseList = expenses.ToList();

        var total = expenseList.Sum(e => e.Amount);

        var breakdown = BuildBreakdown(expenseList, total);

        return new MonthlyReportDto
        {
            Year = year,
            Month = month,
            TotalExpense = total,
            Categories = breakdown
        };
    }

    public async Task<CategorySummaryDto> GetCategorySummaryAsync(ExpenseFilterDto filter)
    {
        if (filter.FromDate.HasValue && filter.ToDate.HasValue && filter.FromDate > filter.ToDate)
            throw new ArgumentException("FromDate cannot be later than ToDate.");

        var expenses = await _expenseRepository.GetFilteredAsync(filter);
        var expenseList = expenses.ToList();

        var total = expenseList.Sum(e => e.Amount);
        var breakdown = BuildBreakdown(expenseList, total);

        return new CategorySummaryDto
        {
            TotalExpense = total,
            Categories = breakdown
        };
    }

    private static List<CategoryBreakdownDto> BuildBreakdown(
        List<Models.Expense> expenses, decimal total)
    {
        return expenses
            .GroupBy(e => new { e.CategoryId, CategoryName = e.Category?.Name ?? "Uncategorized" })
            .Select(g => new CategoryBreakdownDto
            {
                CategoryId = g.Key.CategoryId,
                CategoryName = g.Key.CategoryName,
                Total = g.Sum(e => e.Amount),
                Percentage = total == 0 ? 0 : Math.Round((double)(g.Sum(e => e.Amount) / total) * 100, 2)
            })
            .OrderByDescending(c => c.Total)
            .ToList();
    }
}