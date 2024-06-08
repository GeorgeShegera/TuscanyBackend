using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuscanyBackend.Classes;
using TuscanyBackend.DB;
using TuscanyBackend.DB.Models;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly DbTuscanyContext _context;
        public OrderController()
        {
            _context = new DbTuscanyContext();
        }

        [Authorize]
        [HttpPost]
        [Route("/postOrder")]
        public IActionResult PostOrder([FromBody] Order order)
        {
            try
            {
                order.PaymentMethodNavigation = _context.PaymentMethods.FirstOrDefault(x => x.Id == order.PaymentMethod);
                order.Status = _context.OrderStatuses.FirstOrDefault(x => x.Id == order.StatusId);
                order.TourScheduleNavigation = _context.ToursSchedules.FirstOrDefault(x => x.Id == order.TourSchedule);
                order.Tickets = _context.Tickets.Where(x => x.OrderId == order.Id).ToList();
                _context.Orders.Add(order);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("/getOrders")]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            return _context.Orders.ToList();
        }

        [Authorize]
        [HttpPut]
        [Route("/putOrder")]
        public IActionResult PutOrder([FromBody] Order newOrder)
        {
            try
            {
                Order? order = _context.Orders.First(x => x.Id == newOrder.Id);

                if (order == null)
                    return NotFound();

                // Update existing order properties
                order.TourSchedule = newOrder.TourSchedule;
                order.Price = newOrder.Price;
                order.StatusId = newOrder.StatusId;
                order.PaymentMethod = newOrder.PaymentMethod;

                // Update navigation properties
                order.PaymentMethodNavigation = _context.PaymentMethods.FirstOrDefault(x => x.Id == newOrder.PaymentMethod);
                order.Status = _context.OrderStatuses.FirstOrDefault(x => x.Id == newOrder.StatusId);
                order.TourScheduleNavigation = _context.ToursSchedules.FirstOrDefault(x => x.Id == newOrder.TourSchedule);
                order.Tickets = _context.Tickets.Where(x => x.OrderId == newOrder.Id).ToList();

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        [Route("/deleteOrder")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                Order? order = _context.Orders.FirstOrDefault(x => x.Id == id);
                if (order is null)
                    return NotFound();

                _context.Orders.Remove(order);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }
    }
}
