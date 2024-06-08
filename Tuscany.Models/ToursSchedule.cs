using System;
using System.Collections.Generic;

namespace Tuscany.Models;

public partial class ToursSchedule
{
    public int Id { get; set; }

    public DateTime DateTime { get; set; }

    public int TourId { get; set; }

    public int? ScheduleTypeId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ToursScheduleType? ScheduleType { get; set; }

    public virtual Tour Tour { get; set; } = null!;
}
