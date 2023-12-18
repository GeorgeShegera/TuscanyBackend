using System;
using System.Collections.Generic;

namespace TuscanyBackend.DB.Models;

public partial class PaymentMethod
{
    public int Id { get; set; }

    public string PaymentMethod1 { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
