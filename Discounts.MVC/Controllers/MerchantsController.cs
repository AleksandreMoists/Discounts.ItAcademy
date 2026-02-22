using Discounts.Application.DTOs.Request;
using Discounts.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Discounts.MVC.Controllers;

public class MerchantsController : Controller
{
    private readonly IMerchantService _merchantService;

    public MerchantsController(IMerchantService merchantService)
    {
        _merchantService = merchantService;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var merchants = await _merchantService.GetAllAsync(ct);
        return View(merchants);
    }

    public async Task<IActionResult> Details(int id, CancellationToken ct)
    {
        var merchant = await _merchantService.GetByIdAsync(id, ct);
        return View(merchant);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MerchantCreateDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(dto);

        await _merchantService.CreateAsync(dto, ct);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var merchant = await _merchantService.GetByIdAsync(id, ct);
        return View(new MerchantUpdateDto
        {
            CompanyName = merchant.CompanyName,
            Description = merchant.Description,
            Address = merchant.Address,
            PhoneNumber = merchant.PhoneNumber,
            LogoUrl = merchant.LogoUrl
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, MerchantUpdateDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(dto);

        await _merchantService.UpdateAsync(id, dto, ct);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _merchantService.DeleteAsync(id, ct);
        return RedirectToAction(nameof(Index));
    }
}
