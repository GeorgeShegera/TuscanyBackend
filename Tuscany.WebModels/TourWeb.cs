using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuscany.WebModels
{
    public class TourWeb
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
    }
}
