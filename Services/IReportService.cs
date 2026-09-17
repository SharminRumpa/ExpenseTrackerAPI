using ExpenseTrackerAPI.DTOs.Expenses;
using ExpenseTrackerAPI.DTOs.Reports;

namespace ExpenseTrackerAPI.Services;

public interface IReportService
{
    Task<MonthlyReportDto> GetMonthlyReportAsync(int userId, int year, int month);
    Task<CategorySummaryDto> GetCategorySummaryAsync(ExpenseFilterDto filter);
}