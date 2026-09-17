using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.DTOs.Expenses;

public class ExpenseFilterDto
{
    [Range(1, int.MaxValue, ErrorMessage = "UserId must be a valid positive id.")]
    public int? UserId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be a valid positive id.")]
    public int? CategoryId { get; set; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    [StringLength(200, ErrorMessage = "Search term cannot exceed 200 characters.")]
    public string? Search { get; set; }
}