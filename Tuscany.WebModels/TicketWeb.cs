using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuscany.WebModels
{
    public class TicketWeb
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int OrderId { get; set; }

        public int TypeId { get; set; }
    }
}
