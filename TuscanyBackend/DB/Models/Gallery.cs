using System;
using System.Collections.Generic;

namespace TuscanyBackend.DB.Models;

public partial class Gallery
{
    public int Id { get; set; }

    public string? Img { get; set; }

    public int TourId { get; set; }

    public virtual Tour Tour { get; set; } = null!;
}
