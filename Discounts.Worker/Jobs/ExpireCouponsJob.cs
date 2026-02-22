using Discounts.Domain.Enums;
using Discounts.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Discounts.Worker.Jobs;

public class ExpireCouponsJob : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ExpireCouponsJob> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(30);

    public ExpireCouponsJob(IServiceScopeFactory scopeFactory, ILogger<ExpireCouponsJob> logger)
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

                var expiredCoupons = await db.Coupons
                    .Include(c => c.Offer)
                    .Where(c => c.Status == CouponStatus.Active && c.Offer.EndDate < DateTime.UtcNow)
                    .ToListAsync(stoppingToken);

                if (expiredCoupons.Count > 0)
                {
                    foreach (var coupon in expiredCoupons)
                    {
                        coupon.Status = CouponStatus.Expired;
                        coupon.UpdatedAt = DateTime.UtcNow;
                    }

                    await db.SaveChangesAsync(stoppingToken);
                    _logger.LogInformation("Expired {Count} coupons", expiredCoupons.Count);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error expiring coupons");
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}
