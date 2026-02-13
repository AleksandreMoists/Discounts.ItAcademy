using Discounts.Domain.Enums;

namespace Discounts.Domain.Entities;

public class Coupon : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public decimal Discount { get; set; }
    public CouponStatus Status { get; set; }
    
    public Offer Offer { get; set; } // Navigation property
}