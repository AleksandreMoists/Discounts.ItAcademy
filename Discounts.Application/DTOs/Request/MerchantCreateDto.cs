namespace Discounts.Application.DTOs.Request;

public class MerchantCreateDto
{
    public string CompanyName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public int UserId { get; set; }
}
