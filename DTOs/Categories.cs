using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.DTOs.Categories;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateCategoryDto
{
    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;
}

public class UpdateCategoryDto
{
    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;
}