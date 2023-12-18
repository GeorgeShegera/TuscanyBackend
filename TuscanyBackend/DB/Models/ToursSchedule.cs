using System;
using System.Collections.Generic;

namespace TuscanyBackend.DB.Models;

public partial class ToursSchedule
{
    public int Id { get; set; }

    public DateTime DateTime { get; set; }

    public int TourId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Tour Tour { get; set; } = null!;
}
