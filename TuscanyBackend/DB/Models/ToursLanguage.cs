using System;
using System.Collections.Generic;

namespace TuscanyBackend.DB.Models;

public partial class ToursLanguage
{
    public int Id { get; set; }

    public int LanguageId { get; set; }

    public int TourId { get; set; }

    public virtual Language Language { get; set; } = null!;

    public virtual Tour Tour { get; set; } = null!;
}
