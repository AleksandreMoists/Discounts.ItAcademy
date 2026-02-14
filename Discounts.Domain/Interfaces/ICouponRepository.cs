using Discounts.Domain.Entities;
using Discounts.Domain.Enums;

namespace Discounts.Domain.Interfaces;

public interface ICouponRepository : IBaseRepository<Coupon>
{
    Task<Coupon?> GetByCode(string code, CancellationToken ct = default);
    Task <IEnumerable<Coupon>> GetActiveCoupons(CancellationToken ct = default);
    Task<IEnumerable<Coupon>> GetExpiredCoupons(CancellationToken ct = default);
    Task<IEnumerable<Coupon>> GetCouponsByStatus(CouponStatus status, CancellationToken ct = default);
}