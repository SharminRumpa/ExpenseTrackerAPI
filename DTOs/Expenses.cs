using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.DTOs.Expenses;

public class ExpenseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime ExpenseDate { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateExpenseDto
{
    [Required]
    public int UserId { get; set; }

    [Required(ErrorMessage = "CategoryId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be a valid positive id.")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Amount is required.")]
    [Range(0.01, 100000000, ErrorMessage = "Amount must be greater than 0.")]
    public decimal Amount { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "ExpenseDate is required.")]
    public DateTime ExpenseDate { get; set; }
}

public class UpdateExpenseDto
{
    [Required(ErrorMessage = "CategoryId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be a valid positive id.")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Amount is required.")]
    [Range(0.01, 100000000, ErrorMessage = "Amount must be greater than 0.")]
    public decimal Amount { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "ExpenseDate is required.")]
    public DateTime ExpenseDate { get; set; }
}