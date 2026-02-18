using Discounts.Application.DTOs.Request;
using Discounts.Application.DTOs.Response;
using Discounts.Application.Interfaces;
using Discounts.Domain.Entities;
using Discounts.Domain.Enums;
using Discounts.Domain.Exceptions;
using Discounts.Domain.Interfaces;
using Mapster;

namespace Discounts.Application.Services;

public class OfferService : IOfferService
{
    private readonly IOfferRepository _offerRepository;

    public OfferService(IOfferRepository offerRepository)
    {
        _offerRepository = offerRepository;
    }

    public async Task<OfferResponseDto> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var offer = await _offerRepository.GetById(id, ct);
        if (offer is null)
            throw new NotFoundException($"Offer with ID {id} was not found.");

        return offer.Adapt<OfferResponseDto>();
    }

    public async Task<IEnumerable<OfferResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var offers = await _offerRepository.GetAll(ct);
        return offers.Adapt<IEnumerable<OfferResponseDto>>();
    }

    public async Task<OfferResponseDto> CreateAsync(OfferCreateDto dto, int merchantId, CancellationToken ct = default)
    {
        var offer = dto.Adapt<Offer>();
        offer.MerchantId = merchantId;
        offer.RemainingCoupons = dto.TotalCoupons;
        offer.Status = OfferStatus.Pending;

        var created = await _offerRepository.Add(offer, ct);
        return created.Adapt<OfferResponseDto>();
    }

    public async Task<OfferResponseDto> UpdateAsync(int id, OfferUpdateDto dto, CancellationToken ct = default)
    {
        var offer = await _offerRepository.GetById(id, ct);
        if (offer is null)
            throw new NotFoundException($"Offer with ID {id} was not found.");

        dto.Adapt(offer);
        offer.UpdatedAt = DateTime.UtcNow;

        await _offerRepository.Update(offer);
        return offer.Adapt<OfferResponseDto>();
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var offer = await _offerRepository.GetById(id, ct);
        if (offer is null)
            throw new NotFoundException($"Offer with ID {id} was not found.");

        return await _offerRepository.Delete(offer);
    }

    public async Task<IEnumerable<OfferResponseDto>> GetByCategoryAsync(int categoryId, CancellationToken ct = default)
    {
        var offers = await _offerRepository.GetOffersByCategory(categoryId, ct);
        return offers.Adapt<IEnumerable<OfferResponseDto>>();
    }

    public async Task<IEnumerable<OfferResponseDto>> GetByMerchantAsync(int merchantId, CancellationToken ct = default)
    {
        var offers = await _offerRepository.GetOffersByMerchant(merchantId, ct);
        return offers.Adapt<IEnumerable<OfferResponseDto>>();
    }

    public async Task<IEnumerable<OfferResponseDto>> GetByStatusAsync(OfferStatus status, CancellationToken ct = default)
    {
        var offers = await _offerRepository.GetOffersByStatus(status, ct);
        return offers.Adapt<IEnumerable<OfferResponseDto>>();
    }
}
