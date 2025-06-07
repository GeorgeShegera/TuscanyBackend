using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Tuscany.Models;

public partial class Order
{
    public int Id { get; set; }

    public int TourSchedule { get; set; }

    public decimal? Price { get; set; }

    public int StatusId { get; set; }

    public int? PaymentMethod { get; set; }

    public string? UserId { get; set; }

    public DateTime? DateTime { get; set; }

    public virtual PaymentMethod? PaymentMethodNavigation { get; set; } = null!;

    public virtual OrderStatus? Status { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ToursSchedule? TourScheduleNavigation { get; set; } = null!;

    public virtual User? User { get; set; }
}
