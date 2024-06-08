using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuscany.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int? TourId { get; set; }

        public User User { get; set; } = new User();
        public Tour? Tour { get; set; }

    }
}
