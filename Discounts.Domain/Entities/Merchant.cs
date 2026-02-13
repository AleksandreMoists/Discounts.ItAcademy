using Discounts.Domain.Entities;

public class Merchant : BaseEntity
{
    public string CompanyName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<Offer> Offers { get; set; } = new List<Offer>();
}