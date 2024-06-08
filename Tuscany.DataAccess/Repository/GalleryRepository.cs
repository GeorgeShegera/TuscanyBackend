using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuscany.DataAccess.DB;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.Models;
using Tuscany.WebModels;

namespace Tuscany.DataAccess.Repository
{
    public class GalleryRepository : Repository<Gallery>, IGalleryRepository
    {
        public GalleryRepository(DbTuscanyContext db)
            : base(db) { }

        public void Update(Gallery gallery)
        {
            _db.Update(gallery);
        }
    }
}
