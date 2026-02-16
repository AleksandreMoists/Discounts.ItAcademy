namespace Discounts.Application.DTOs.Request;

public class ReservationCreateDto
{
    public int OfferId { get; set; }
    public int CustomerId { get; set; }
    public int Quantity { get; set; }
}
