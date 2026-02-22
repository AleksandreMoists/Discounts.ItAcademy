using Discounts.Application.DTOs.Request;
using Discounts.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Discounts.MVC.Controllers;

public class CouponsController : Controller
{
    private readonly ICouponService _couponService;

    public CouponsController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var coupons = await _couponService.GetAllAsync(ct);
        return View(coupons);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CouponCreateDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(dto);

        await _couponService.CreateAsync(dto, ct);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var coupon = await _couponService.GetByIdAsync(id, ct);
        return View(new CouponUpdateDto
        {
            Code = coupon.Code,
            Discount = coupon.Discount,
            OfferId = 0
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CouponUpdateDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(dto);

        await _couponService.UpdateAsync(id, dto, ct);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _couponService.DeleteAsync(id, ct);
        return RedirectToAction(nameof(Index));
    }
}
