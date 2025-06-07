using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Tuscany.DataAccess.DB;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.Models;
using Tuscany.Utility;
using Tuscany.WebModels;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpPost]
        [Route("/postOrder")]
        public IActionResult PostOrder([FromBody] OrderRequest order)
        {
            try
            {
                // Decode parameter Liqpay response data and converting to dictionary 
                var responseData = Convert.FromBase64String(order.Data);
                var decodedString = Encoding.UTF8.GetString(responseData);
                var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedString);
                bool parsed = int.TryParse(jsonResponse?["order_id"].Split("_")[1], out int orderId);

                if (parsed)
                {
                    Order? orderDbo = _unitOfWork.Order.Get(o => o.Id == orderId);
                    if (orderDbo is not null)
                    {
                        orderDbo.StatusId = SD.OrderUpcoming;
                    }
                    _unitOfWork.Save();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("/getLiqpay")]
        public ActionResult<LiqpayViewModel> GetLiqpay(int? orderId,
            string userId,
            decimal amount,
            int tourScheduleId,
            int childTickets,
            int adultTickets,
            int infantTickets,
            decimal adultPrice,
            decimal childPrice,
            string? tourName = null)
        {
            Order? order = _unitOfWork.Order.Get(x => x.Id == orderId);

            if (order is null)
            {
                order = new Order()
                {
                    UserId = userId,
                    Price = amount,
                    TourSchedule = tourScheduleId,
                    StatusId = SD.OrderPendingId,
                    DateTime = DateTime.Now,
                };
                order = _unitOfWork.Order.AddOrderReturn(order);
            }

            Dictionary<string, int> ticketCounts = _unitOfWork.Ticket.GetTicketsCount(order.Id);

            IList<Ticket> tickets = new List<Ticket>();

            TicketsUtility.Tickets(_unitOfWork, adultTickets, ticketCounts["adult"], "adult", adultPrice, order.Id);
            TicketsUtility.Tickets(_unitOfWork, childTickets, ticketCounts["child"], "child", childPrice, order.Id);
            TicketsUtility.Tickets(_unitOfWork, infantTickets, ticketCounts["infant"], "infant", 0, order.Id);

            LiqpayViewModel liqpayService = LiqpayService.GetLiqpayModel(orderId ?? order.Id, amount, tourName);
            return Ok(liqpayService);
        }

        [Authorize]
        [HttpGet]
        [Route("/getOrders")]
        public ActionResult<IEnumerable<OrderWeb>> GetOrders()
        {
            List<Order> orders = _unitOfWork.Order.GetAll().ToList();
            return orders.Select(x => new OrderWeb
            {
                Id = x.Id,
                TourSchedule = x.TourSchedule,
                StatusId = x.StatusId,
                PaymentMethod = x.PaymentMethod,
                DateTime = x.DateTime,
                UserId = x.UserId,
            }).ToList();
        }

        [Authorize]
        [HttpGet]
        [Route("/getUserTickets")]
        public IActionResult GetUserTickets(string userId)
        {
            IEnumerable<UserOrder> orders = (from o in _unitOfWork.Order.GetAll()
                                             join u in _unitOfWork.User.GetAll() on o.UserId equals u.Id
                                             join sh in _unitOfWork.ToursSchedule.GetAll() on o.TourSchedule equals sh.Id
                                             join pm in _unitOfWork.PaymentMethod.GetAll()
                                             on o.PaymentMethod equals pm.Id into orderGroup
                                             from orderWithPayments in orderGroup.DefaultIfEmpty()
                                             join t in _unitOfWork.Tour.GetAll() on sh.TourId equals t.Id
                                             join os in _unitOfWork.OrderStatus.GetAll() on o.StatusId equals os.Id
                                             where u.Id == userId
                                             select new UserOrder
                                             (
                                               tourName: t.Name,
                                               dateTime: o.DateTime ?? sh.DateTime,
                                               img: _unitOfWork.Gallery.GetAll()
                                                                       .Where(g => g.TourId == t.Id)
                                                                       .ToList()[0].Img ?? "",
                                               paymentMethod: orderWithPayments?.Method,
                                               price: o.Price ?? 0,
                                               status: os.Status
                                             ));

            return Ok(orders);
        }

        [Authorize]
        [HttpPut]
        [Route("/putOrder")]
        public IActionResult PutOrder([FromBody] OrderWeb orderWeb)
        {
            try
            {
                Order order = new()
                {
                    Id = orderWeb.Id,
                    TourSchedule = orderWeb.TourSchedule,
                    Price = SD.CountPrice(orderWeb),
                    StatusId = orderWeb.StatusId,
                    PaymentMethod = orderWeb.PaymentMethod,
                    UserId = orderWeb.UserId,
                };

                _unitOfWork.Order.Update(order);
                _unitOfWork.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }


        //[Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        [Route("/deleteOrder")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                Order? order = _unitOfWork.Order.Get(x => x.Id == id);
                if (order is null)
                    return NotFound();

                _unitOfWork.Order.Remove(order);
                _unitOfWork.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }
    }
}
