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

        public decimal? Price { get; set; }

        public int StatusId { get; set; }

        public int PaymentMethod { get; set; }

        public string? UserId { get; set; }

    }
}
