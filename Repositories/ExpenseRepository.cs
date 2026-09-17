using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.DTOs.Expenses;
using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly ApplicationDbContext _context;

    public ExpenseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Expense>> GetAllAsync()
    {
        return await _context.Expenses
            .Include(e => e.Category)
            .OrderByDescending(e => e.ExpenseDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Expense>> GetByUserIdAsync(int userId)
    {
        return await _context.Expenses
            .Include(e => e.Category)
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.ExpenseDate)
            .ToListAsync();
    }

    public async Task<Expense?> GetByIdAsync(int id)
    {
        return await _context.Expenses
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddAsync(Expense expense)
    {
        await _context.Expenses.AddAsync(expense);
    }

    public void Update(Expense expense)
    {
        _context.Expenses.Update(expense);
    }

    public void Delete(Expense expense)
    {
        _context.Expenses.Remove(expense);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Expense>> GetFilteredAsync(ExpenseFilterDto filter)
    {
        var query = _context.Expenses
            .Include(e => e.Category)
            .AsQueryable();

        if (filter.UserId.HasValue)
            query = query.Where(e => e.UserId == filter.UserId.Value);

        if (filter.CategoryId.HasValue)
            query = query.Where(e => e.CategoryId == filter.CategoryId.Value);

        if (filter.FromDate.HasValue)
            query = query.Where(e => e.ExpenseDate >= filter.FromDate.Value.Date);

        if (filter.ToDate.HasValue)
            query = query.Where(e => e.ExpenseDate <= filter.ToDate.Value.Date);

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var term = filter.Search.Trim().ToLower();
            query = query.Where(e =>
                (e.Description != null && e.Description.ToLower().Contains(term)) ||
                e.Category!.Name.ToLower().Contains(term));
        }

        return await query
            .OrderByDescending(e => e.ExpenseDate)
            .ToListAsync();
    }
}