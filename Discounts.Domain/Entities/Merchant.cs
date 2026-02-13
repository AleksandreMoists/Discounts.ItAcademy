namespace Discounts.Domain.Entities;

public class Merchant : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
}