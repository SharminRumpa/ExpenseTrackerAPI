using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, MaxLength(60)]
        public string Name { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    
    }
}
