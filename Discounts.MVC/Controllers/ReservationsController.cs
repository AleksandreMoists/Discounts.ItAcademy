using Discounts.Application.DTOs.Request;
using Discounts.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Discounts.MVC.Controllers;

public class ReservationsController : Controller
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var reservations = await _reservationService.GetAllAsync(ct);
        return View(reservations);
    }

    public async Task<IActionResult> Details(int id, CancellationToken ct)
    {
        var reservation = await _reservationService.GetByIdAsync(id, ct);
        return View(reservation);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ReservationCreateDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(dto);

        await _reservationService.CreateAsync(dto, ct);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _reservationService.DeleteAsync(id, ct);
        return RedirectToAction(nameof(Index));
    }
}
