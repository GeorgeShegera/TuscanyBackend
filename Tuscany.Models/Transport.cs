using System;
using System.Collections.Generic;

namespace Tuscany.Models;

public partial class Transport
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
