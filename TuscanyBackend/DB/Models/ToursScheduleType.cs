using System;
using System.Collections.Generic;

namespace TuscanyBackend.DB.Models;

public partial class ToursScheduleType
{
    public int Id { get; set; }

    public string ScheduleType { get; set; } = null!;
}
