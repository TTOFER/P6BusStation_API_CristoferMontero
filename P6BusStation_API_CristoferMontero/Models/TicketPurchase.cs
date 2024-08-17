using System;
using System.Collections.Generic;

namespace P6BusStation_API_CristoferMontero.Models;

public partial class TicketPurchase
{
    public int TicketPurchaseId { get; set; }

    public int ScheduleId { get; set; }

    public int UserId { get; set; }

    public string Seat { get; set; } = null!;

    public DateTime PurchaseDate { get; set; }

    public string TicketStatus { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Schedule Schedule { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
