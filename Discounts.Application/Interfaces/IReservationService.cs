using Discounts.Application.DTOs.Request;
using Discounts.Application.DTOs.Response;
using Discounts.Domain.Enums;

namespace Discounts.Application.Interfaces;

public interface IReservationService
{
    Task<ReservationResponseDto> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<ReservationResponseDto>> GetAllAsync(CancellationToken ct = default);
    Task<ReservationResponseDto> CreateAsync(ReservationCreateDto dto, CancellationToken ct = default);
    Task<ReservationResponseDto> UpdateAsync(int id, ReservationUpdateDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<ReservationResponseDto>> GetByCustomerAsync(int customerId, CancellationToken ct = default);
    Task<IEnumerable<ReservationResponseDto>> GetByOfferAsync(int offerId, CancellationToken ct = default);
    Task<IEnumerable<ReservationResponseDto>> GetByStatusAsync(ReservationStatus status, CancellationToken ct = default);
}
