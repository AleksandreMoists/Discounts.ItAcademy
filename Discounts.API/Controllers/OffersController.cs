using Discounts.Application.DTOs.Request;
using Discounts.Application.Interfaces;
using Discounts.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Discounts.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OffersController : ControllerBase
{
    private readonly IOfferService _offerService;

    public OffersController(IOfferService offerService)
    {
        _offerService = offerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var offers = await _offerService.GetAllAsync(ct);
        return Ok(offers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var offer = await _offerService.GetByIdAsync(id, ct);
        return Ok(offer);
    }

    [HttpPost("{merchantId:int}")]
    public async Task<IActionResult> Create(int merchantId, [FromBody] OfferCreateDto dto, CancellationToken ct)
    {
        var offer = await _offerService.CreateAsync(dto, merchantId, ct);
        return CreatedAtAction(nameof(GetById), new { id = offer.Id }, offer);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] OfferUpdateDto dto, CancellationToken ct)
    {
        var offer = await _offerService.UpdateAsync(id, dto, ct);
        return Ok(offer);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _offerService.DeleteAsync(id, ct);
        return NoContent();
    }

    [HttpGet("category/{categoryId:int}")]
    public async Task<IActionResult> GetByCategory(int categoryId, CancellationToken ct)
    {
        var offers = await _offerService.GetByCategoryAsync(categoryId, ct);
        return Ok(offers);
    }

    [HttpGet("merchant/{merchantId:int}")]
    public async Task<IActionResult> GetByMerchant(int merchantId, CancellationToken ct)
    {
        var offers = await _offerService.GetByMerchantAsync(merchantId, ct);
        return Ok(offers);
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(OfferStatus status, CancellationToken ct)
    {
        var offers = await _offerService.GetByStatusAsync(status, ct);
        return Ok(offers);
    }
}
