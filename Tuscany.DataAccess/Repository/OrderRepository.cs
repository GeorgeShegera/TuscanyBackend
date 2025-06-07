using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DbTuscanyContext db) : base(db)
        {
        }

        public void Update(Order order)
        {
            _db.Update(order);
        }

        public Order AddOrderReturn(Order order)
        {
            EntityEntry<Order> orderEntity = _db.Add(order);
            _db.SaveChanges();
            return orderEntity.Entity;
        }
    }
}
