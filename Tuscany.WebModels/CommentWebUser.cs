using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuscany.WebModels
{
    public class CommentWebUser
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int? TourId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
