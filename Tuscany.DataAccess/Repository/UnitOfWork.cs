using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuscany.DataAccess.DB;
using Tuscany.DataAccess.Repository.IRepository;

namespace Tuscany.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbTuscanyContext _db;

        public ICommentRepository Comment { get; private set; }

        public IGalleryRepository Gallery { get; private set; }

        public ILanguageRepository Language { get; private set; }

        public IOrderRepository Order { get; private set; }

        public IOrderStatusRepository OrderStatus { get; private set; }

        public IPaymentMethodRepository PaymentMethod { get; private set; }

        public ITicketRepository Ticket { get; private set; }

        public ITicketTypeRepository TicketType { get; private set; }

        public ITourRepository Tour { get; private set; }

        public IToursLanguageRepository ToursLanguage { get; private set; }

        public IToursScheduleRepository ToursSchedule { get; private set; }

        public IToursScheduleTypeRepository ToursScheduleType { get; private set; }

        public ITransportRepository Transport { get; private set; }

        public IUserRepository User { get; private set; }

        public UnitOfWork(DbTuscanyContext db)
        {
            _db = db;
            Comment = new CommentRepository(db);
            Gallery = new GalleryRepository(_db);
            Language = new LanguageRepository(_db);
            Order = new OrderRepository(_db);
            OrderStatus = new OrderStatusRepository(_db);
            PaymentMethod = new PaymentMethodRepository(_db);
            Ticket = new TicketRepository(_db);
            TicketType = new TicketTypeRepository(_db);
            Tour = new TourRepository(_db);
            ToursLanguage = new ToursLanguageRepository(_db);
            ToursSchedule = new ToursScheduleRepository(_db);
            ToursScheduleType = new ToursScheduleTypeRepository(_db);
            Transport = new TransportRepository(_db);
            User = new UserRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
