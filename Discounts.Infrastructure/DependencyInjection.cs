using Microsoft.Extensions.DependencyInjection;

namespace Discounts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register infrastructure services here as needed:
        // - Email/notification services
        // - External API clients
        // - Caching (Redis, etc.)
        // - Authentication/JWT services
        // - File storage services

        return services;
    }
}
