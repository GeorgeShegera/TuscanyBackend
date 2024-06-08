using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuscany.WebModels
{
    public class CommentWeb
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int? TourId { get; set; }
    }
}
