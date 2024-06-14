using System;
using System.Collections.Generic;

namespace P6BusStation_API_CristoferMontero.Models;

public partial class Destination
{
    public int DestinationId { get; set; }

    public string DestinationName { get; set; } = null!;

    public virtual ICollection<Route> RouteFinalDestinations { get; set; } = new List<Route>();

    public virtual ICollection<Route> RouteOriginDestinations { get; set; } = new List<Route>();
}
