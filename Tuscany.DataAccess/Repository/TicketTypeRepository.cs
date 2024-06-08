using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuscany.DataAccess.DB;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.Models;

namespace Tuscany.DataAccess.Repository
{
    public class TicketTypeRepository : Repository<TicketsType>, ITicketTypeRepository
    {
        public TicketTypeRepository(DbTuscanyContext db) : base(db)
        {
        }

        public void Update(TicketsType ticketType)
        {
            throw new NotImplementedException();
        }
    }
}
