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
    public class TransportRepository : Repository<Transport>, ITransportRepository
    {
        public TransportRepository(DbTuscanyContext db) : base(db)
        {
        }

        public void Update(Transport transport)
        {
            _db.Update(transport);
        }
    }
}
