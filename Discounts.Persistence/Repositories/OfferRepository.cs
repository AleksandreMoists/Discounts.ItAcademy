using Discounts.Domain.Entities;
using Discounts.Domain.Enums;
using Discounts.Domain.Interfaces;
using Discounts.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Discounts.Persistence.Repositories;

public class OfferRepository : BaseRepository<Offer>, IOfferRepository
{
    public OfferRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Offer>> GetOffersByCategory(int categoryId, CancellationToken ct = default)
    {
        return await DbSet
            .Include(o => o.Category)
            .Include(o => o.Merchant)
            .Where(o => o.CategoryId == categoryId)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Offer>> GetOffersByMerchant(int merchantId, CancellationToken ct = default)
    {
        return await DbSet
            .Include(o => o.Category)
            .Include(o => o.Merchant)
            .Where(o => o.MerchantId == merchantId)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Offer>> GetOffersByStatus(OfferStatus status, CancellationToken ct = default)
    {
        return await DbSet
            .Include(o => o.Category)
            .Include(o => o.Merchant)
            .Where(o => o.Status == status)
            .ToListAsync(ct);
    }
}
