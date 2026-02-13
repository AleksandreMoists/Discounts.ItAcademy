namespace Discounts.Domain.Entities;

public class Offers
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public int TotalCoupons { get; set; }
    public int RemainingCoupons { get; set; }
    public int MerchantId { get; set; } // Foreign Key
    public int CategoryId { get; set; } // Foreign Key
    public string? ImageUrl { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public Merchant Merchant { get; set; } // Navigation Property
    public Category Category { get; set; } // Navigation Property

    public ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

}