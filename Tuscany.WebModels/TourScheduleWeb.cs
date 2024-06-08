using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuscany.WebModels
{
    public class TourScheduleWeb
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public int TourId { get; set; }

        public int? ScheduleTypeId { get; set; }
    }
}
