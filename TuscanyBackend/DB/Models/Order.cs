using System;
using System.Collections.Generic;

namespace TuscanyBackend.DB.Models;

public partial class Order
{
    public int Id { get; set; }

    public string ReceiverName { get; set; } = null!;

    public string ReceiverSurname { get; set; } = null!;

    public string ReceiverPhoneNumber { get; set; } = null!;

    public string ReceiverEmail { get; set; } = null!;

    public int TourSchedule { get; set; }

    public decimal? Price { get; set; }

    public int StatusId { get; set; }

    public int PaymentMethod { get; set; }

    public virtual PaymentMethod PaymentMethodNavigation { get; set; } = null!;

    public virtual OrderStatus Status { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ToursSchedule TourScheduleNavigation { get; set; } = null!;
}
