using Discounts.Domain.Entities;
using Discounts.Domain.Enums;
using Discounts.Domain.Interfaces;
using Discounts.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Discounts.Persistence.Repositories;

public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Reservation>> GetReservationsByStatus(ReservationStatus status, CancellationToken ct = default)
    {
        return await DbSet
            .Include(r => r.Offer)
            .Include(r => r.User)
            .Where(r => r.Status == status)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByMerchant(int merchantId, CancellationToken ct = default)
    {
        return await DbSet
            .Include(r => r.Offer)
            .Include(r => r.User)
            .Where(r => r.Offer.MerchantId == merchantId)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByCustomer(int customerId, CancellationToken ct = default)
    {
        return await DbSet
            .Include(r => r.Offer)
            .Include(r => r.User)
            .Where(r => r.CustomerId == customerId)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByOffer(int offerId, CancellationToken ct = default)
    {
        return await DbSet
            .Include(r => r.Offer)
            .Include(r => r.User)
            .Where(r => r.OfferId == offerId)
            .ToListAsync(ct);
    }
}
