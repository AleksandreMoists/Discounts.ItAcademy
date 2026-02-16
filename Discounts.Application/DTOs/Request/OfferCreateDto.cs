namespace Discounts.Application.DTOs.Request;

public class OfferCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public int TotalCoupons { get; set; }
    public int CategoryId { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}