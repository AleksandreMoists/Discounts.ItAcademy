using Discounts.Domain.Entities;
using Discounts.Domain.Enums;

namespace Discounts.Domain.Interfaces;

public interface IReservationRepository : IBaseRepository<Reservation>
{
    Task<IEnumerable<Reservation>> GetReservationsByStatus(ReservationStatus status, CancellationToken ct = default);
    Task<IEnumerable<Reservation>> GetReservationsByMerchant(int merchantId, CancellationToken ct = default);
    Task<IEnumerable<Reservation>> GetReservationsByCustomer(int customerId, CancellationToken ct = default);
    Task<IEnumerable<Reservation>> GetReservationsByOffer(int offerId, CancellationToken ct = default);
}