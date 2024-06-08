using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuscany.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICommentRepository Comment { get; }
        IGalleryRepository Gallery { get; }
        ILanguageRepository Language { get; }
        IOrderRepository Order { get; }
        IOrderStatusRepository OrderStatus { get; }
        IPaymentMethodRepository PaymentMethod { get; }
        ITicketRepository Ticket { get; }
        ITicketTypeRepository TicketType { get; }
        ITourRepository Tour { get; }
        IToursLanguageRepository ToursLanguage { get; }
        IToursScheduleRepository ToursSchedule { get; }
        IToursScheduleTypeRepository ToursScheduleType { get; }
        ITransportRepository Transport { get; }
        IUserRepository User { get; }

        void Save();
    }
}
