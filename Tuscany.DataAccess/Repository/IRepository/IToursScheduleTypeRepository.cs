using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuscany.Models;

namespace Tuscany.DataAccess.Repository.IRepository
{
    public interface IToursScheduleTypeRepository : IRepository<ToursScheduleType>
    {
        void Update(ToursScheduleType scheduleType);
    }
}
