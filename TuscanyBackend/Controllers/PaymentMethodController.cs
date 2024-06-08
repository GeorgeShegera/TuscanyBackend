using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuscanyBackend.Classes;
using TuscanyBackend.DB;
using TuscanyBackend.DB.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TuscanyBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly DbTuscanyContext _context;

        public PaymentMethodController()
        {
            _context = new DbTuscanyContext();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postPaymentMethod")]
        public IActionResult Post([FromBody] PaymentMethod paymentMethod)
        {
            try
            {
                paymentMethod.Orders = _context.Orders.Where(x => x.PaymentMethod == paymentMethod.Id).ToList();
                _context.PaymentMethods.Add(paymentMethod);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("/getPaymentMethods")]
        public ActionResult<IEnumerable<PaymentMethod>> GetPaymentMethods()
        {
            return _context.PaymentMethods.ToList();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putPaymentMethod")]
        public IActionResult PutPaymentMethod([FromBody] PaymentMethod newPaymentMethod)
        {
            try
            {
                PaymentMethod? paymentMethod = _context.PaymentMethods.First(x => x.Id == newPaymentMethod.Id);

                if (paymentMethod == null)
                    return NotFound();

                paymentMethod.Method = newPaymentMethod.Method;
                paymentMethod.Orders = newPaymentMethod.Orders.Where(x => x.PaymentMethod == newPaymentMethod.Id).ToList();

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
        [Route("/deletePaymentMethod")]
        public IActionResult DeletePaymentMethod(int id)
        {
            try
            {
                PaymentMethod? paymentMethod = _context.PaymentMethods.First(x => x.Id == id);
                if (paymentMethod is null)
                    return NotFound();

                _context.PaymentMethods.Remove(paymentMethod);
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
