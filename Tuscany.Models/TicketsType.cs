using System;
using System.Collections.Generic;

namespace Tuscany.Models;

public partial class TicketsType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
