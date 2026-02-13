using Discounts.Domain.Enums;

namespace Discounts.Domain.Entities;

public class Reservation : BaseEntity
{
    public int OfferId { get; set; }
    public int CustomerId { get; set; }
    public DateTime ReservationDate { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public ReservationStatus Status { get; set; }
    
    public Offer Offer { get; set; } // Navigation Property
    public User User { get; set; } // Navigation Property
}