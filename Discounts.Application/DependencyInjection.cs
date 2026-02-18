using Discounts.Application.Interfaces;
using Discounts.Application.Mapping;
using Discounts.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Discounts.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IOfferService, OfferService>();
        services.AddScoped<ICouponService, CouponService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IMerchantService, MerchantService>();
        services.AddScoped<IUserService, UserService>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        MappingConfig.RegisterMappings();

        return services;
    }
}
