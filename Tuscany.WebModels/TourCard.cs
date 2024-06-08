namespace Tuscany.WebModels
{
    public class TourCard
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Img { get; set; }
        public string? ScheduleType { get; set; }
        public int MinNumberOfGroup { get; set; }
        public int MaxNumberOfGroup { get; set; }
        public string? Description { get; set; }
    }
}
