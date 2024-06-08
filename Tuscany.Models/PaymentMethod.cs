using System;
using System.Collections.Generic;

namespace Tuscany.Models;

public partial class PaymentMethod
{
    public int Id { get; set; }

    public string Method { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
