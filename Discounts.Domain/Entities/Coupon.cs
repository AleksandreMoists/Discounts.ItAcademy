namespace Discounts.Domain.Entities;

public class Coupon : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public decimal Discount { get; set; }
}