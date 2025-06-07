using Microsoft.EntityFrameworkCore;
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
    public class TourRepository : Repository<Tour>, ITourRepository
    {
        public TourRepository(DbTuscanyContext db) : base(db)
        {
        }

        public void Update(Tour tour)
        {
            _db.Update(tour);
        }

        public List<Tour> GetAllToursWithComments()
            => [.. _db.Tours.Include(t => t.Comments)];
    }
}
