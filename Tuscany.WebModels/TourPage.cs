using Tuscany.WebModels;
using Tuscany.Models;

namespace Tuscany.WebModels
{
    public class TourPage
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? AdultPrice { get; set; }
        public decimal? ChildPrice { get; set; }
        public string? Description { get; set; }
        public List<ScheduleDateTypeMod>? Schedules { get; set; }
        public string? Details { get; set; }
        public int MinNumberOfGroup { get; set; }
        public int MaxNumberOfGroup { get; set; }
        public int Duration { get; set; }
        public string? DepartureArea { get; set; }
        public bool GuideService { get; set; }
        public List<string>? Languages { get; set; }
        public decimal? EntryFees { get; set; }
        public string? Transport { get; set; }
        public List<string>? Imgs { get; set; }

        public TourPage(Tour? tour, List<ScheduleDateTypeMod>? schedules, List<string>? languages,
                        string? transport, List<string>? imgs)
        {
            Id = tour.Id;
            Name = tour.Name;
            AdultPrice = tour.AdultPrice;
            ChildPrice = tour.ChildPrice;
            Description = tour.Description;
            Schedules = schedules;
            Details = tour.Details;
            MinNumberOfGroup = tour.MinNumberOfGroup;
            MaxNumberOfGroup = tour.MaxNumberOfGroup;
            Duration = tour.Duration;
            DepartureArea = tour.DepAndArrivAreas;
            GuideService = tour.GuideService;
            Languages = languages;
            EntryFees = tour.EntryFees;
            Transport = transport;
            Imgs = imgs;
        }
    }
}
