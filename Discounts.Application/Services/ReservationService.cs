using Discounts.Application.DTOs.Request;
using Discounts.Application.DTOs.Response;
using Discounts.Application.Interfaces;
using Discounts.Domain.Entities;
using Discounts.Domain.Enums;
using Discounts.Domain.Exceptions;
using Discounts.Domain.Interfaces;
using Mapster;

namespace Discounts.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IOfferRepository _offerRepository;

    public ReservationService(IReservationRepository reservationRepository, IOfferRepository offerRepository)
    {
        _reservationRepository = reservationRepository;
        _offerRepository = offerRepository;
    }

    public async Task<ReservationResponseDto> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var reservation = await _reservationRepository.GetById(id, ct);
        if (reservation is null)
            throw new NotFoundException($"Reservation with ID {id} was not found.");

        return reservation.Adapt<ReservationResponseDto>();
    }

    public async Task<IEnumerable<ReservationResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var reservations = await _reservationRepository.GetAll(ct);
        return reservations.Adapt<IEnumerable<ReservationResponseDto>>();
    }

    public async Task<ReservationResponseDto> CreateAsync(ReservationCreateDto dto, CancellationToken ct = default)
    {
        var offer = await _offerRepository.GetById(dto.OfferId, ct);
        if (offer is null)
            throw new NotFoundException($"Offer with ID {dto.OfferId} was not found.");

        if (offer.RemainingCoupons < dto.Quantity)
            throw new ValidationException("Not enough coupons remaining for this offer.");

        var reservation = dto.Adapt<Reservation>();
        reservation.ReservationDate = DateTime.UtcNow;
        reservation.TotalPrice = offer.DiscountPrice * dto.Quantity;
        reservation.Status = ReservationStatus.Active;

        offer.RemainingCoupons -= dto.Quantity;
        await _offerRepository.Update(offer);

        var created = await _reservationRepository.Add(reservation, ct);
        return created.Adapt<ReservationResponseDto>();
    }

    public async Task<ReservationResponseDto> UpdateAsync(int id, ReservationUpdateDto dto, CancellationToken ct = default)
    {
        var reservation = await _reservationRepository.GetById(id, ct);
        if (reservation is null)
            throw new NotFoundException($"Reservation with ID {id} was not found.");

        dto.Adapt(reservation);
        reservation.UpdatedAt = DateTime.UtcNow;

        await _reservationRepository.Update(reservation);
        return reservation.Adapt<ReservationResponseDto>();
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var reservation = await _reservationRepository.GetById(id, ct);
        if (reservation is null)
            throw new NotFoundException($"Reservation with ID {id} was not found.");

        return await _reservationRepository.Delete(reservation);
    }

    public async Task<IEnumerable<ReservationResponseDto>> GetByCustomerAsync(int customerId, CancellationToken ct = default)
    {
        var reservations = await _reservationRepository.GetReservationsByCustomer(customerId, ct);
        return reservations.Adapt<IEnumerable<ReservationResponseDto>>();
    }

    public async Task<IEnumerable<ReservationResponseDto>> GetByOfferAsync(int offerId, CancellationToken ct = default)
    {
        var reservations = await _reservationRepository.GetReservationsByOffer(offerId, ct);
        return reservations.Adapt<IEnumerable<ReservationResponseDto>>();
    }

    public async Task<IEnumerable<ReservationResponseDto>> GetByStatusAsync(ReservationStatus status, CancellationToken ct = default)
    {
        var reservations = await _reservationRepository.GetReservationsByStatus(status, ct);
        return reservations.Adapt<IEnumerable<ReservationResponseDto>>();
    }
}
