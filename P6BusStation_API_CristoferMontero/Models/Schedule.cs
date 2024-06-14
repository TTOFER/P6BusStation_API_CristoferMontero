using System;
using System.Collections.Generic;

namespace P6BusStation_API_CristoferMontero.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int RouteId { get; set; }

    public DateTime AvailableDate { get; set; }

    public string? WeekDayName { get; set; }

    public short ArrivalHour { get; set; }

    public short StartHour { get; set; }

    public int Capacity { get; set; }

    public decimal Price { get; set; }

    public virtual Route Route { get; set; } = null!;

    public virtual ICollection<TicketPurchase> TicketPurchases { get; set; } = new List<TicketPurchase>();
}
