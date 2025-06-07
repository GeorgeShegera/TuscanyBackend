using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuscany.WebModels
{
    public class TourCardsDto
    {
        public int PagPage { get; set; }
        public int CardsCount { get; set; }
        public string? SearchName { get; set; }
    }
}
