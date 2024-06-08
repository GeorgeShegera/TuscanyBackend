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
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(DbTuscanyContext db) : base(db)
        {
        }

        public void Update(Ticket ticket)
        {
            _db.Update(ticket);
        }
    }
}
