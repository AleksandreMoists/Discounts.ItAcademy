namespace Discounts.Application.DTOs.Response;

public class ReservationResponseDto
{
    public int Id { get; set; }
    public int OfferId { get; set; }
    public string OfferName { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public DateTime ReservationDate { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
