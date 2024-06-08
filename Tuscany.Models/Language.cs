using System;
using System.Collections.Generic;

namespace Tuscany.Models;

public partial class Language
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ToursLanguage> ToursLanguages { get; set; } = new List<ToursLanguage>();
}
