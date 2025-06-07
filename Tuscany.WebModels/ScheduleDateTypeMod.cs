using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuscany.WebModels
{
    public class ScheduleDateTypeMod(int id, DateTime dateTime, string? type)
    {
        public int Id { get; set; } = id;
        public DateTime DateTime { get; set; } = dateTime;
        public string? Type { get; set; } = type;
    }
}
