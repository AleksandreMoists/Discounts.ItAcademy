using Discounts.Application.DTOs.Request;
using Discounts.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Discounts.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MerchantsController : ControllerBase
{
    private readonly IMerchantService _merchantService;

    public MerchantsController(IMerchantService merchantService)
    {
        _merchantService = merchantService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var merchants = await _merchantService.GetAllAsync(ct);
        return Ok(merchants);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var merchant = await _merchantService.GetByIdAsync(id, ct);
        return Ok(merchant);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive(CancellationToken ct)
    {
        var merchants = await _merchantService.GetActiveMerchantsAsync(ct);
        return Ok(merchants);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MerchantCreateDto dto, CancellationToken ct)
    {
        var merchant = await _merchantService.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = merchant.Id }, merchant);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] MerchantUpdateDto dto, CancellationToken ct)
    {
        var merchant = await _merchantService.UpdateAsync(id, dto, ct);
        return Ok(merchant);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _merchantService.DeleteAsync(id, ct);
        return NoContent();
    }
}
