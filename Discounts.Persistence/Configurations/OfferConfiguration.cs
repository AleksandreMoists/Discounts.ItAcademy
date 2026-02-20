using Discounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Discounts.Persistence.Configurations;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(o => o.Description)
            .HasMaxLength(1000);

        builder.Property(o => o.OriginalPrice)
            .HasPrecision(18, 2);

        builder.Property(o => o.DiscountPrice)
            .HasPrecision(18, 2);

        builder.Property(o => o.ImageUrl)
            .HasMaxLength(500);

        builder.Property(o => o.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasOne(o => o.Merchant)
            .WithMany(m => m.Offers)
            .HasForeignKey(o => o.MerchantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Category)
            .WithMany()
            .HasForeignKey(o => o.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
