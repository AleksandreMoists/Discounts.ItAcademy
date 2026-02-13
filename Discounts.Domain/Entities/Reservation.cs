namespace Discounts.Domain.Entities;

public class Reservation : BaseEntity
{
    public int OfferId { get; set; }
    public int CustomerId { get; set; }
    public DateTime ReservationDate { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}