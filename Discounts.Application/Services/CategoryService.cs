using Discounts.Application.DTOs.Request;
using Discounts.Application.DTOs.Response;
using Discounts.Application.Interfaces;
using Discounts.Domain.Entities;
using Discounts.Domain.Exceptions;
using Discounts.Domain.Interfaces;
using Mapster;

namespace Discounts.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryResponseDto> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var category = await _categoryRepository.GetById(id, ct);
        if (category is null)
            throw new NotFoundException($"Category with ID {id} was not found.");

        return category.Adapt<CategoryResponseDto>();
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var categories = await _categoryRepository.GetAll(ct);
        return categories.Adapt<IEnumerable<CategoryResponseDto>>();
    }

    public async Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto, CancellationToken ct = default)
    {
        var category = dto.Adapt<Category>();
        var created = await _categoryRepository.Add(category, ct);
        return created.Adapt<CategoryResponseDto>();
    }

    public async Task<CategoryResponseDto> UpdateAsync(int id, CategoryUpdateDto dto, CancellationToken ct = default)
    {
        var category = await _categoryRepository.GetById(id, ct);
        if (category is null)
            throw new NotFoundException($"Category with ID {id} was not found.");

        dto.Adapt(category);
        category.UpdatedAt = DateTime.UtcNow;

        await _categoryRepository.Update(category);
        return category.Adapt<CategoryResponseDto>();
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var category = await _categoryRepository.GetById(id, ct);
        if (category is null)
            throw new NotFoundException($"Category with ID {id} was not found.");

        return await _categoryRepository.Delete(category);
    }
}
