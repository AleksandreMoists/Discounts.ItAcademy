using Discounts.Domain.Entities;
using Discounts.Domain.Enums;
using Discounts.Domain.Interfaces;
using Discounts.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Discounts.Persistence.Repositories;

public class CouponRepository : BaseRepository<Coupon>, ICouponRepository
{
    public CouponRepository(AppDbContext context) : base(context) { }

    public async Task<Coupon?> GetByCode(string code, CancellationToken ct = default)
    {
        return await DbSet
            .Include(c => c.Offer)
            .FirstOrDefaultAsync(c => c.Code == code, ct);
    }

    public async Task<IEnumerable<Coupon>> GetActiveCoupons(CancellationToken ct = default)
    {
        return await DbSet
            .Include(c => c.Offer)
            .Where(c => c.Status == CouponStatus.Active)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Coupon>> GetExpiredCoupons(CancellationToken ct = default)
    {
        return await DbSet
            .Include(c => c.Offer)
            .Where(c => c.Status == CouponStatus.Expired)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Coupon>> GetCouponsByStatus(CouponStatus status, CancellationToken ct = default)
    {
        return await DbSet
            .Include(c => c.Offer)
            .Where(c => c.Status == status)
            .ToListAsync(ct);
    }
}
