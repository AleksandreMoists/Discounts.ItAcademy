using Discounts.Domain.Enums;
using Discounts.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Discounts.Worker.Jobs;

public class ExpireOffersJob : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ExpireOffersJob> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(30);

    public ExpireOffersJob(IServiceScopeFactory scopeFactory, ILogger<ExpireOffersJob> logger)
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

                var expiredOffers = await db.Offers
                    .Where(o => o.EndDate < DateTime.UtcNow && o.Status == OfferStatus.Approved)
                    .ToListAsync(stoppingToken);

                if (expiredOffers.Count > 0)
                {
                    foreach (var offer in expiredOffers)
                    {
                        offer.Status = OfferStatus.Rejected;
                        offer.UpdatedAt = DateTime.UtcNow;
                    }

                    await db.SaveChangesAsync(stoppingToken);
                    _logger.LogInformation("Expired {Count} offers", expiredOffers.Count);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error expiring offers");
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}
