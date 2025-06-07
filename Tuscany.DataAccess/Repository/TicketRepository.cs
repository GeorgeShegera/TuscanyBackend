using System;
using System.Collections;
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

        public void RemoveLastOfType(int ticketTypeId)
        {
            Ticket? entity = dbSet.Where(t => t.TypeId == ticketTypeId)
                                    .OrderBy(x => x.Id)
                                   .LastOrDefault();

            if (entity is not null)
            {
                dbSet.Remove(entity);
            }
        }

        public Dictionary<string, int> GetTicketsCount(int orderId)
        {
            return new(
                [
                    new KeyValuePair<string, int>("adult", _db.Tickets.Count(t => t.OrderId == orderId && t.TypeId == 1)),
                    new KeyValuePair<string, int>("child", _db.Tickets.Count(t => t.OrderId == orderId && t.TypeId == 2)),
                    new KeyValuePair<string, int>("infant", _db.Tickets.Count(t => t.OrderId == orderId && t.TypeId == 3))
                ]
            );
        }
    }
}
