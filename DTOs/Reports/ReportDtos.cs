namespace ExpenseTrackerAPI.DTOs.Reports;

public class MonthlyReportDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal TotalExpense { get; set; }
    public List<CategoryBreakdownDto> Categories { get; set; } = new();
}

public class CategorySummaryDto
{
    public decimal TotalExpense { get; set; }
    public List<CategoryBreakdownDto> Categories { get; set; } = new();
}

public class CategoryBreakdownDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public double Percentage { get; set; }
}