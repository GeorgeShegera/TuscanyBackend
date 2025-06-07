using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tuscany.DataAccess.DB;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.Models;
using Tuscany.Utility;
using Tuscany.WebModels;

namespace TuscanyBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderStatusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postOrderStatus")]
        public IActionResult Post([FromBody] OrderStatus orderStatusWeb)
        {
            try
            {
                OrderStatus orderStatus = new()
                {
                    Status = orderStatusWeb.Status
                };
                _unitOfWork.OrderStatus.Add(orderStatus);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/getOrderStatuses")]
        public ActionResult<IEnumerable<OrderStatusWeb>> GetOrders()
        {
            List<OrderStatus> orderStatuses = _unitOfWork.OrderStatus.GetAll().ToList();
            return orderStatuses.Select(x => new OrderStatusWeb
            {
                Id = x.Id,
                Status = x.Status,
            }).ToList();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putOrderStatus")]
        public IActionResult PutOrderStatus([FromBody] OrderStatusWeb orderStatusWeb)
        {
            try
            {
                OrderStatus orderStatus = new()
                {
                    Id = orderStatusWeb.Id,
                    Status = orderStatusWeb.Status
                };

                _unitOfWork.OrderStatus.Update(orderStatus);
                _unitOfWork.Save();

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
                OrderStatus? orderStatus = _unitOfWork.OrderStatus.Get(x => x.Id == id);
                if (orderStatus is null)
                    return NotFound();

                _unitOfWork.OrderStatus.Remove(orderStatus);
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
