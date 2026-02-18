using Discounts.Application.DTOs.Request;
using Discounts.Application.DTOs.Response;
using Discounts.Domain.Enums;

namespace Discounts.Application.Interfaces;

public interface IOfferService
{
    Task<OfferResponseDto> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<OfferResponseDto>> GetAllAsync(CancellationToken ct = default);
    Task<OfferResponseDto> CreateAsync(OfferCreateDto dto, int merchantId, CancellationToken ct = default);
    Task<OfferResponseDto> UpdateAsync(int id, OfferUpdateDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<OfferResponseDto>> GetByCategoryAsync(int categoryId, CancellationToken ct = default);
    Task<IEnumerable<OfferResponseDto>> GetByMerchantAsync(int merchantId, CancellationToken ct = default);
    Task<IEnumerable<OfferResponseDto>> GetByStatusAsync(OfferStatus status, CancellationToken ct = default);
}
