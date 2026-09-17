using ExpenseTrackerAPI.DTOs.Expenses;

namespace ExpenseTrackerAPI.Services;

public interface IExpenseService
{
    Task<IEnumerable<ExpenseDto>> GetAllAsync();
    Task<IEnumerable<ExpenseDto>> GetByUserIdAsync(int userId);
    Task<ExpenseDto?> GetByIdAsync(int id);
    Task<ExpenseDto> CreateAsync(CreateExpenseDto dto);
    Task<bool> UpdateAsync(int id, UpdateExpenseDto dto);
    Task<bool> DeleteAsync(int id);

    Task<IEnumerable<ExpenseDto>> GetFilteredAsync(ExpenseFilterDto filter);
}