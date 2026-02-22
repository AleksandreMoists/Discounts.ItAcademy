using Discounts.Application.DTOs.Request;
using Discounts.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Discounts.MVC.Controllers;

public class OffersController : Controller
{
    private readonly IOfferService _offerService;
    private readonly ICategoryService _categoryService;
    private readonly IMerchantService _merchantService;

    public OffersController(IOfferService offerService, ICategoryService categoryService, IMerchantService merchantService)
    {
        _offerService = offerService;
        _categoryService = categoryService;
        _merchantService = merchantService;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var offers = await _offerService.GetAllAsync(ct);
        return View(offers);
    }

    public async Task<IActionResult> Details(int id, CancellationToken ct)
    {
        var offer = await _offerService.GetByIdAsync(id, ct);
        return View(offer);
    }

    public async Task<IActionResult> Create(CancellationToken ct)
    {
        await PopulateDropdowns(ct);
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OfferCreateDto dto, int merchantId, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropdowns(ct);
            return View(dto);
        }

        await _offerService.CreateAsync(dto, merchantId, ct);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var offer = await _offerService.GetByIdAsync(id, ct);
        await PopulateDropdowns(ct);
        return View(new OfferUpdateDto
        {
            Name = offer.Name,
            Description = offer.Description,
            OriginalPrice = offer.OriginalPrice,
            DiscountPrice = offer.DiscountPrice,
            TotalCoupons = offer.TotalCoupons,
            CategoryId = 0,
            ImageUrl = offer.ImageUrl,
            StartDate = offer.StartDate,
            EndDate = offer.EndDate
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, OfferUpdateDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropdowns(ct);
            return View(dto);
        }

        await _offerService.UpdateAsync(id, dto, ct);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _offerService.DeleteAsync(id, ct);
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateDropdowns(CancellationToken ct)
    {
        var categories = await _categoryService.GetAllAsync(ct);
        var merchants = await _merchantService.GetAllAsync(ct);
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        ViewBag.Merchants = new SelectList(merchants, "Id", "CompanyName");
    }
}
