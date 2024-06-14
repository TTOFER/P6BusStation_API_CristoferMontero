using System;
using System.Collections.Generic;

namespace P6BusStation_API_CristoferMontero.Models;

public partial class Route
{
    public int RouteId { get; set; }

    public int OriginDestinationId { get; set; }

    public int FinalDestinationId { get; set; }

    public virtual Destination FinalDestination { get; set; } = null!;

    public virtual Destination OriginDestination { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
