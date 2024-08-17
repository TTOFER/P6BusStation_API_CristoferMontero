using System;
using System.Collections.Generic;

namespace P6BusStation_API_CristoferMontero.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int TicketPurchaseId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string TransactionId { get; set; } = null!;

    public virtual TicketPurchase TicketPurchase { get; set; } = null!;
}
