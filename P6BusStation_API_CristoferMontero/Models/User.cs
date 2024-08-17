using System;
using System.Collections.Generic;

namespace P6BusStation_API_CristoferMontero.Models;

public partial class User
{
    public int UserId { get; set; }

    public int UserRoleId { get; set; }

    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string PhoneNumber { get; set; }

    public string UserPassword { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public virtual ICollection<TicketPurchase> TicketPurchases { get; set; } = new List<TicketPurchase>();

    public virtual UserRole UserRole { get; set; } = null!;
}
