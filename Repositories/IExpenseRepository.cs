using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Repositories;

public interface IExpenseRepository
{
    Task<IEnumerable<Expense>> GetAllAsync();
    Task<IEnumerable<Expense>> GetByUserIdAsync(int userId);
    Task<Expense?> GetByIdAsync(int id);
    Task AddAsync(Expense expense);
    void Update(Expense expense);
    void Delete(Expense expense);
    Task<bool> SaveChangesAsync();
}