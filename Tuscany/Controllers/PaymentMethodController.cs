using Microsoft.AspNetCore.Authorization;
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
    public class PaymentMethodController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentMethodController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postPaymentMethod")]
        public IActionResult Post([FromBody] PaymentMethodWeb paymentMethodWeb)
        {
            try
            {
                PaymentMethod paymentMethod = new()
                {
                    Method = paymentMethodWeb.Method,
                };

                _unitOfWork.PaymentMethod.Add(paymentMethod);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpGet]
        [Route("/getPaymentMethods")]
        public ActionResult<IEnumerable<PaymentMethodWeb>> GetPaymentMethods()
        {
            List<PaymentMethod> paymentMethods = _unitOfWork.PaymentMethod.GetAll().ToList();
            return paymentMethods.Select(x => new PaymentMethodWeb
            {
                Id = x.Id,
                Method = x.Method,
            }).ToList();
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putPaymentMethod")]
        public IActionResult PutPaymentMethod([FromBody] PaymentMethodWeb paymentMethodWeb)
        {
            try
            {
                PaymentMethod paymentMethod = new()
                {
                    Id = paymentMethodWeb.Id,
                    Method = paymentMethodWeb.Method,
                };

                _unitOfWork.PaymentMethod.Update(paymentMethod);
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
        [Route("/deletePaymentMethod")]
        public IActionResult DeletePaymentMethod(int id)
        {
            try
            {
                PaymentMethod? paymentMethod = _unitOfWork.PaymentMethod.Get(x => x.Id == id);
                if (paymentMethod is null)
                    return NotFound();

                _unitOfWork.PaymentMethod.Remove(paymentMethod);
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
