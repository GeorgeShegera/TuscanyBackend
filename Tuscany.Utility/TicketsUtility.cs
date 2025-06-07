using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuscany.DataAccess.Repository;
using Tuscany.DataAccess.Repository.IRepository;

namespace Tuscany.Utility
{
    public static class TicketsUtility
    {
        public static void Tickets(IUnitOfWork unitOfWork, int expectedTickets,
                                    int actualTickets,
                                    string ticketType,
                                    decimal price,
                                    int orderId)
        {
            int ticketTypeId = 0;
            switch (ticketType)
            {
                case "adult":
                    ticketTypeId = 1;
                    break;
                case "child":
                    ticketTypeId = 2;
                    break;
                case "infant":
                    ticketTypeId = 3;
                    break;

            }
            

            for (int i = Math.Abs(expectedTickets - actualTickets); i > 0 ; i--)
            {
                if (actualTickets < expectedTickets)
                {
                    unitOfWork.Ticket.Add(new Models.Ticket
                    {
                        Price = price,
                        OrderId = orderId,
                        TypeId = ticketTypeId,
                    });
                }
                else
                {
                    unitOfWork.Ticket.RemoveLastOfType(ticketTypeId);
                }
            }
            unitOfWork.Save();
        }
    }
}
