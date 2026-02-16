namespace Discounts.Application.DTOs.Response;

public class CouponResponseDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public decimal Discount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string OfferName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
