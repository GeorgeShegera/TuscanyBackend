using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tuscany.WebModels;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.Models;
using Tuscany.Utility;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postTransport")]
        public IActionResult PostOrder([FromBody] TransportWeb transportWeb)
        {
            try
            {
                Transport transport = new()
                {
                    Name = transportWeb.Name
                };
                _unitOfWork.Transport.Add(transport);
                _unitOfWork.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("/getTransports")]
        public ActionResult<IEnumerable<TransportWeb>> GetTransports()
        {
            List<Transport> transports = _unitOfWork.Transport.GetAll().ToList();
            return transports.Select(x => new TransportWeb()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putTransport")]
        public IActionResult PutTour([FromBody] TransportWeb transportWeb)
        {
            try
            {
                Transport? transport = new()
                {
                    Id = transportWeb.Id,
                    Name = transportWeb.Name
                };

                _unitOfWork.Transport.Update(transport);
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
        [Route("/deleteTransport")]
        public IActionResult DeleteTransport(int id)
        {
            try
            {
                Transport? transport = _unitOfWork.Transport.Get(x => x.Id == id);

                if (transport is null)
                    return NotFound();

                _unitOfWork.Transport.Remove(transport);
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
