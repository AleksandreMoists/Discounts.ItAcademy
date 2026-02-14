using Discounts.Domain.Entities;
using Discounts.Domain.Enums;

namespace Discounts.Domain.Interfaces;

public interface IOfferRepository : IBaseRepository<Offer>
{
    Task<IEnumerable<Offer>> GetOffersByCategory(int categoryId, CancellationToken ct = default);
    Task<IEnumerable<Offer>> GetOffersByMerchant(int merchantId, CancellationToken ct = default);
    Task<IEnumerable<Offer>> GetOffersByStatus(OfferStatus status, CancellationToken ct = default);
}