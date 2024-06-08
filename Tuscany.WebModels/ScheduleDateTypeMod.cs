using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuscany.WebModels
{
    public class ScheduleDateTypeMod(DateTime dateTime, string? type)
    {
        public DateTime DateTime { get; set; } = dateTime;
        public string? Type { get; set; } = type;
    }
}
