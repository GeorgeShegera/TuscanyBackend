using System;
using System.Collections.Generic;

namespace Tuscany.Models;

public partial class Tour
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Details { get; set; }

    public string? Description { get; set; }

    public int MinNumberOfGroup { get; set; }

    public int MaxNumberOfGroup { get; set; }

    public int TransportId { get; set; }

    public int Duration { get; set; }

    public bool GuideService { get; set; }

    public string DepAndArrivAreas { get; set; } = null!;

    public decimal? EntryFees { get; set; }

    public decimal? AdultPrice { get; set; }

    public decimal? ChildPrice { get; set; }

    public virtual ICollection<Gallery> Galleries { get; set; } = new List<Gallery>();

    public virtual ICollection<ToursLanguage> ToursLanguages { get; set; } = new List<ToursLanguage>();

    public virtual ICollection<ToursSchedule> ToursSchedules { get; set; } = new List<ToursSchedule>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Transport? Transport { get; set; } = null;
}
