using System;
using System.Collections.Generic;

namespace TuscanyBackend.DB.Models;

public partial class TicketsType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;
}
