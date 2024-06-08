using System;
using System.Collections.Generic;

namespace Tuscany.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public decimal Price { get; set; }

    public int OrderId { get; set; }

    public int TypeId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual TicketsType Type { get; set; } = null!;
}
