using Discounts.Application.DTOs.Request;
using Discounts.Application.DTOs.Response;
using Discounts.Application.Interfaces;
using Discounts.Domain.Entities;
using Discounts.Domain.Enums;
using Discounts.Domain.Exceptions;
using Discounts.Domain.Interfaces;
using Mapster;

namespace Discounts.Application.Services;

public class CouponService : ICouponService
{
    private readonly ICouponRepository _couponRepository;

    public CouponService(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    public async Task<CouponResponseDto> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var coupon = await _couponRepository.GetById(id, ct);
        if (coupon is null)
            throw new NotFoundException($"Coupon with ID {id} was not found.");

        return coupon.Adapt<CouponResponseDto>();
    }

    public async Task<IEnumerable<CouponResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var coupons = await _couponRepository.GetAll(ct);
        return coupons.Adapt<IEnumerable<CouponResponseDto>>();
    }

    public async Task<CouponResponseDto> CreateAsync(CouponCreateDto dto, CancellationToken ct = default)
    {
        var coupon = dto.Adapt<Coupon>();
        coupon.Status = CouponStatus.Active;

        var created = await _couponRepository.Add(coupon, ct);
        return created.Adapt<CouponResponseDto>();
    }

    public async Task<CouponResponseDto> UpdateAsync(int id, CouponUpdateDto dto, CancellationToken ct = default)
    {
        var coupon = await _couponRepository.GetById(id, ct);
        if (coupon is null)
            throw new NotFoundException($"Coupon with ID {id} was not found.");

        dto.Adapt(coupon);
        coupon.UpdatedAt = DateTime.UtcNow;

        await _couponRepository.Update(coupon);
        return coupon.Adapt<CouponResponseDto>();
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var coupon = await _couponRepository.GetById(id, ct);
        if (coupon is null)
            throw new NotFoundException($"Coupon with ID {id} was not found.");

        return await _couponRepository.Delete(coupon);
    }

    public async Task<CouponResponseDto?> GetByCodeAsync(string code, CancellationToken ct = default)
    {
        var coupon = await _couponRepository.GetByCode(code, ct);
        return coupon?.Adapt<CouponResponseDto>();
    }

    public async Task<IEnumerable<CouponResponseDto>> GetByStatusAsync(CouponStatus status, CancellationToken ct = default)
    {
        var coupons = await _couponRepository.GetCouponsByStatus(status, ct);
        return coupons.Adapt<IEnumerable<CouponResponseDto>>();
    }
}
