using ExpenseTrackerAPI.DTOs.Categories;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Repositories;

namespace ExpenseTrackerAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories.Select(MapToDto);
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        return category is null ? null : MapToDto(category);
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var existing = await _repository.GetByNameAsync(dto.Name);
        if (existing is not null)
            throw new InvalidOperationException($"Category '{dto.Name}' already exists.");

        var category = new Category
        {
            Name = dto.Name.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();

        return MapToDto(category);
    }

    public async Task<bool> UpdateAsync(int id, UpdateCategoryDto dto)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category is null)
            return false;

        var existing = await _repository.GetByNameAsync(dto.Name);
        if (existing is not null && existing.Id != id)
            throw new InvalidOperationException($"Category '{dto.Name}' already exists.");

        category.Name = dto.Name.Trim();
        _repository.Update(category);
        return await _repository.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category is null)
            return false;

        _repository.Delete(category);
        return await _repository.SaveChangesAsync();
    }

    private static CategoryDto MapToDto(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            CreatedAt = category.CreatedAt
        };
    }
}