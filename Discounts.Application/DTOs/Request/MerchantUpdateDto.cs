namespace Discounts.Application.DTOs.Request;

public class MerchantUpdateDto
{
    public string CompanyName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
}
