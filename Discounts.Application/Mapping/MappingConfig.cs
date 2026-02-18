using Discounts.Application.DTOs.Response;
using Discounts.Domain.Entities;
using Mapster;

namespace Discounts.Application.Mapping;

public static class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Offer, OfferResponseDto>.NewConfig()
            .Map(dest => dest.Status, src => src.Status.ToString())
            .Map(dest => dest.CategoryName, src => src.Category != null ? src.Category.Name : string.Empty)
            .Map(dest => dest.MerchantName, src => src.Merchant != null ? src.Merchant.CompanyName : string.Empty);

        TypeAdapterConfig<Coupon, CouponResponseDto>.NewConfig()
            .Map(dest => dest.Status, src => src.Status.ToString())
            .Map(dest => dest.OfferName, src => src.Offer != null ? src.Offer.Name : string.Empty);

        TypeAdapterConfig<Reservation, ReservationResponseDto>.NewConfig()
            .Map(dest => dest.Status, src => src.Status.ToString())
            .Map(dest => dest.OfferName, src => src.Offer != null ? src.Offer.Name : string.Empty);

        TypeAdapterConfig<User, UserResponseDto>.NewConfig()
            .Map(dest => dest.Role, src => src.Role.ToString());
    }
}
