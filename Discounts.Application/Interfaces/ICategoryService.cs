using Discounts.Application.DTOs.Request;
using Discounts.Application.DTOs.Response;

namespace Discounts.Application.Interfaces;

public interface ICategoryService
{
    Task<CategoryResponseDto> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<CategoryResponseDto>> GetAllAsync(CancellationToken ct = default);
    Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto, CancellationToken ct = default);
    Task<CategoryResponseDto> UpdateAsync(int id, CategoryUpdateDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
