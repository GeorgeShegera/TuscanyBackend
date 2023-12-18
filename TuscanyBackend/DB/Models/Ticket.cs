using System;
using System.Collections.Generic;

namespace TuscanyBackend.DB.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public decimal Price { get; set; }

    public int OrderId { get; set; }

    public int TypeId { get; set; }

    public virtual ICollection<Ticket> InverseType { get; set; } = new List<Ticket>();

    public virtual Order Order { get; set; } = null!;

    public virtual Ticket Type { get; set; } = null!;
}
