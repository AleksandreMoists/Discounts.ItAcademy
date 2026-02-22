using Discounts.Domain.Enums;
using Discounts.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Discounts.Worker.Jobs;

public class ExpireReservationsJob : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ExpireReservationsJob> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(30);

    public ExpireReservationsJob(IServiceScopeFactory scopeFactory, ILogger<ExpireReservationsJob> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var expiredReservations = await db.Reservations
                    .Include(r => r.Offer)
                    .Where(r => r.Status == ReservationStatus.Active && r.Offer.EndDate < DateTime.UtcNow)
                    .ToListAsync(stoppingToken);

                if (expiredReservations.Count > 0)
                {
                    foreach (var reservation in expiredReservations)
                    {
                        reservation.Status = ReservationStatus.Expired;
                        reservation.UpdatedAt = DateTime.UtcNow;
                    }

                    await db.SaveChangesAsync(stoppingToken);
                    _logger.LogInformation("Expired {Count} reservations", expiredReservations.Count);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error expiring reservations");
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}
