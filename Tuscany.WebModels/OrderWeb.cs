using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuscany.WebModels
{
    public class OrderWeb
    {
        public int Id { get; set; }

        public int TourSchedule { get; set; }

        public int StatusId { get; set; }

        public int? PaymentMethod { get; set; }

        public DateTime? DateTime { get; set; }

        public string? UserId { get; set; }
        public decimal AdultPrice { get; set; }
        public decimal ChildPrice { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int InfantCount { get; set; }
    }
}
