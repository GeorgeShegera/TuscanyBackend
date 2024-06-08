using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //[Authorize]
        [HttpPost]
        [Route("/postOrder")]
        public IActionResult PostOrder([FromBody] OrderWeb orderWeb)
        {
            try
            {
                Order order = new()
                {
                    TourSchedule = orderWeb.TourSchedule,
                    Price = orderWeb.Price,
                    StatusId = orderWeb.StatusId,
                    PaymentMethod = orderWeb.PaymentMethod,
                    UserId = orderWeb.UserId,
                };
                _unitOfWork.Order.Add(order);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        //[Authorize]
        [HttpGet]
        [Route("/getOrders")]
        public ActionResult<IEnumerable<OrderWeb>> GetOrders()
        {
            List<Order> orders = _unitOfWork.Order.GetAll().ToList();
            return orders.Select(x => new OrderWeb
            {
                Id = x.Id,
                TourSchedule = x.TourSchedule,
                Price = x.Price,
                StatusId = x.StatusId,
                PaymentMethod = x.PaymentMethod,
                UserId = x.UserId,
            }).ToList();
        }

        //[Authorize]
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
                    Price = orderWeb.Price,
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
