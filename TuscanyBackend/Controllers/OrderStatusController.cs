using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuscanyBackend.Classes;
using TuscanyBackend.DB;
using TuscanyBackend.DB.Models;

namespace TuscanyBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly DbTuscanyContext _context;
        public OrderStatusController()
        {
            _context = new DbTuscanyContext();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postOrderStatus")]
        public IActionResult Post([FromBody] OrderStatus status)
        {
            try
            {
                status.Orders = _context.Orders.Where(x => x.StatusId == status.Id).ToList();
                _context.OrderStatuses.Add(status);
                _context.SaveChanges();
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        [Route("/getOrderStatuses")]
        public ActionResult<IEnumerable<OrderStatus>> GetOrders()
        {
            return _context.OrderStatuses.ToList();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putOrderStatus")]
        public IActionResult PutOrderStatus([FromBody] OrderStatus newOrderStatus)
        {
            try
            {
                OrderStatus? order = _context.OrderStatuses.First(x => x.Id == newOrderStatus.Id);

                if (order == null)
                    return NotFound();

                order.Status = newOrderStatus.Status;
                order.Orders = _context.Orders.Where(x => x.StatusId == newOrderStatus.Id).ToList();

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
        [Route("/deleteOrderStatus")]
        public IActionResult DeleteOrderStatus(int id)
        {
            try
            {
                OrderStatus? orderStatus = _context.OrderStatuses.First(x => x.Id == id);
                if (orderStatus is null)
                    return NotFound();

                _context.OrderStatuses.Remove(orderStatus);
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
