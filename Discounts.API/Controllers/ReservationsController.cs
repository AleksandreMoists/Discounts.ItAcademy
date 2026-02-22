using Discounts.Application.DTOs.Request;
using Discounts.Application.Interfaces;
using Discounts.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Discounts.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var reservations = await _reservationService.GetAllAsync(ct);
        return Ok(reservations);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var reservation = await _reservationService.GetByIdAsync(id, ct);
        return Ok(reservation);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReservationCreateDto dto, CancellationToken ct)
    {
        var reservation = await _reservationService.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ReservationUpdateDto dto, CancellationToken ct)
    {
        var reservation = await _reservationService.UpdateAsync(id, dto, ct);
        return Ok(reservation);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _reservationService.DeleteAsync(id, ct);
        return NoContent();
    }

    [HttpGet("customer/{customerId:int}")]
    public async Task<IActionResult> GetByCustomer(int customerId, CancellationToken ct)
    {
        var reservations = await _reservationService.GetByCustomerAsync(customerId, ct);
        return Ok(reservations);
    }

    [HttpGet("offer/{offerId:int}")]
    public async Task<IActionResult> GetByOffer(int offerId, CancellationToken ct)
    {
        var reservations = await _reservationService.GetByOfferAsync(offerId, ct);
        return Ok(reservations);
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(ReservationStatus status, CancellationToken ct)
    {
        var reservations = await _reservationService.GetByStatusAsync(status, ct);
        return Ok(reservations);
    }
}
