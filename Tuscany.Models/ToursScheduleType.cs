using System;
using System.Collections.Generic;

namespace Tuscany.Models;

public partial class ToursScheduleType
{
    public int Id { get; set; }

    public string ScheduleType { get; set; } = null!;

    public virtual ICollection<ToursSchedule> ToursSchedules { get; set; } = new List<ToursSchedule>();
}
