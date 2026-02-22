using Discounts.Application.DTOs.Request;
using Discounts.Application.Interfaces;
using Discounts.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Discounts.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CouponsController : ControllerBase
{
    private readonly ICouponService _couponService;

    public CouponsController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var coupons = await _couponService.GetAllAsync(ct);
        return Ok(coupons);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var coupon = await _couponService.GetByIdAsync(id, ct);
        return Ok(coupon);
    }

    [HttpGet("code/{code}")]
    public async Task<IActionResult> GetByCode(string code, CancellationToken ct)
    {
        var coupon = await _couponService.GetByCodeAsync(code, ct);
        if (coupon is null)
            return NotFound();
        return Ok(coupon);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CouponCreateDto dto, CancellationToken ct)
    {
        var coupon = await _couponService.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = coupon.Id }, coupon);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CouponUpdateDto dto, CancellationToken ct)
    {
        var coupon = await _couponService.UpdateAsync(id, dto, ct);
        return Ok(coupon);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _couponService.DeleteAsync(id, ct);
        return NoContent();
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(CouponStatus status, CancellationToken ct)
    {
        var coupons = await _couponService.GetByStatusAsync(status, ct);
        return Ok(coupons);
    }
}
