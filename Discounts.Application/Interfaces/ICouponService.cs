using Discounts.Application.DTOs.Request;
using Discounts.Application.DTOs.Response;
using Discounts.Domain.Enums;

namespace Discounts.Application.Interfaces;

public interface ICouponService
{
    Task<CouponResponseDto> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<CouponResponseDto>> GetAllAsync(CancellationToken ct = default);
    Task<CouponResponseDto> CreateAsync(CouponCreateDto dto, CancellationToken ct = default);
    Task<CouponResponseDto> UpdateAsync(int id, CouponUpdateDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    Task<CouponResponseDto?> GetByCodeAsync(string code, CancellationToken ct = default);
    Task<IEnumerable<CouponResponseDto>> GetByStatusAsync(CouponStatus status, CancellationToken ct = default);
}
