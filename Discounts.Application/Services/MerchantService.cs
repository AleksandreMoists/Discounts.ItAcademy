using Discounts.Application.DTOs.Request;
using Discounts.Application.DTOs.Response;
using Discounts.Application.Interfaces;
using Discounts.Domain.Entities;
using Discounts.Domain.Exceptions;
using Discounts.Domain.Interfaces;
using Mapster;

namespace Discounts.Application.Services;

public class MerchantService : IMerchantService
{
    private readonly IMerchantRepository _merchantRepository;

    public MerchantService(IMerchantRepository merchantRepository)
    {
        _merchantRepository = merchantRepository;
    }

    public async Task<MerchantResponseDto> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var merchant = await _merchantRepository.GetById(id, ct);
        if (merchant is null)
            throw new NotFoundException($"Merchant with ID {id} was not found.");

        return merchant.Adapt<MerchantResponseDto>();
    }

    public async Task<IEnumerable<MerchantResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var merchants = await _merchantRepository.GetAll(ct);
        return merchants.Adapt<IEnumerable<MerchantResponseDto>>();
    }

    public async Task<MerchantResponseDto> CreateAsync(MerchantCreateDto dto, CancellationToken ct = default)
    {
        var merchant = dto.Adapt<Merchant>();
        merchant.IsActive = true;

        var created = await _merchantRepository.Add(merchant, ct);
        return created.Adapt<MerchantResponseDto>();
    }

    public async Task<MerchantResponseDto> UpdateAsync(int id, MerchantUpdateDto dto, CancellationToken ct = default)
    {
        var merchant = await _merchantRepository.GetById(id, ct);
        if (merchant is null)
            throw new NotFoundException($"Merchant with ID {id} was not found.");

        dto.Adapt(merchant);
        merchant.UpdatedAt = DateTime.UtcNow;

        await _merchantRepository.Update(merchant);
        return merchant.Adapt<MerchantResponseDto>();
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var merchant = await _merchantRepository.GetById(id, ct);
        if (merchant is null)
            throw new NotFoundException($"Merchant with ID {id} was not found.");

        return await _merchantRepository.Delete(merchant);
    }

    public async Task<IEnumerable<MerchantResponseDto>> GetActiveMerchantsAsync(CancellationToken ct = default)
    {
        var merchants = await _merchantRepository.GetActiveMerchants(ct);
        return merchants.Adapt<IEnumerable<MerchantResponseDto>>();
    }
}
