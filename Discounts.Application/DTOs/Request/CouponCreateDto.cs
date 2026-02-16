namespace Discounts.Application.DTOs.Request;

public class CouponCreateDto
{
    public string Code { get; set; } = string.Empty;
    public decimal Discount { get; set; }
    public int OfferId { get; set; }
}
