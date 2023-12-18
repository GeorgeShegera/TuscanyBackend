using System;
using System.Collections.Generic;

namespace TuscanyBackend.DB.Models;

public partial class Language
{
    public int Id { get; set; }

    public string Language1 { get; set; } = null!;

    public virtual ICollection<ToursLanguage> ToursLanguages { get; set; } = new List<ToursLanguage>();
}
