using ExpenseTrackerAPI.DTOs.Expenses;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Repositories;

namespace ExpenseTrackerAPI.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ExpenseService(
        IExpenseRepository expenseRepository,
        ICategoryRepository categoryRepository)
    {
        _expenseRepository = expenseRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<ExpenseDto>> GetAllAsync()
    {
        var expenses = await _expenseRepository.GetAllAsync();
        return expenses.Select(MapToDto);
    }

    public async Task<IEnumerable<ExpenseDto>> GetByUserIdAsync(int userId)
    {
        var expenses = await _expenseRepository.GetByUserIdAsync(userId);
        return expenses.Select(MapToDto);
    }

    public async Task<ExpenseDto?> GetByIdAsync(int id)
    {
        var expense = await _expenseRepository.GetByIdAsync(id);
        return expense is null ? null : MapToDto(expense);
    }

    public async Task<ExpenseDto> CreateAsync(CreateExpenseDto dto)
    {
        if (dto.Amount < 0)
            throw new ArgumentException("Amount cannot be negative.");

        var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
        if (category is null)
            throw new KeyNotFoundException($"Category with id {dto.CategoryId} not found.");

        var expense = new Expense
        {
            UserId = dto.UserId,
            CategoryId = dto.CategoryId,
            Amount = dto.Amount,
            Description = dto.Description,
            ExpenseDate = dto.ExpenseDate,
            CreatedAt = DateTime.UtcNow
        };

        await _expenseRepository.AddAsync(expense);
        await _expenseRepository.SaveChangesAsync();

        expense.Category = category;
        return MapToDto(expense);
    }

    public async Task<bool> UpdateAsync(int id, UpdateExpenseDto dto)
    {
        if (dto.Amount < 0)
            throw new ArgumentException("Amount cannot be negative.");

        var expense = await _expenseRepository.GetByIdAsync(id);
        if (expense is null)
            return false;

        var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
        if (category is null)
            throw new KeyNotFoundException($"Category with id {dto.CategoryId} not found.");

        expense.CategoryId = dto.CategoryId;
        expense.Amount = dto.Amount;
        expense.Description = dto.Description;
        expense.ExpenseDate = dto.ExpenseDate;

        _expenseRepository.Update(expense);
        return await _expenseRepository.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var expense = await _expenseRepository.GetByIdAsync(id);
        if (expense is null)
            return false;

        _expenseRepository.Delete(expense);
        return await _expenseRepository.SaveChangesAsync();
    }

    private static ExpenseDto MapToDto(Expense expense)
    {
        return new ExpenseDto
        {
            Id = expense.Id,
            UserId = expense.UserId,
            CategoryId = expense.CategoryId,
            CategoryName = expense.Category?.Name ?? string.Empty,
            Amount = expense.Amount,
            Description = expense.Description,
            ExpenseDate = expense.ExpenseDate,
            CreatedAt = expense.CreatedAt
        };
    }
}