using Discounts.Application.DTOs.Request;
using Discounts.Application.DTOs.Response;

namespace Discounts.Application.Interfaces;

public interface IMerchantService
{
    Task<MerchantResponseDto> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<MerchantResponseDto>> GetAllAsync(CancellationToken ct = default);
    Task<MerchantResponseDto> CreateAsync(MerchantCreateDto dto, CancellationToken ct = default);
    Task<MerchantResponseDto> UpdateAsync(int id, MerchantUpdateDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<MerchantResponseDto>> GetActiveMerchantsAsync(CancellationToken ct = default);
}
