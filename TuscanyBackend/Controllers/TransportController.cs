using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuscanyBackend.Classes;
using TuscanyBackend.DB;
using TuscanyBackend.DB.Models;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransportController : Controller
    {
        private readonly DbTuscanyContext _context;

        public TransportController()
        {
            _context = new DbTuscanyContext();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postTransport")]
        public IActionResult PostOrder([FromBody] Transport transport)
        {
            try
            {
                transport.Tours = _context.Tours.Where(x => x.TransportId == transport.Id).ToList();
                _context.Transports.Add(transport);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("/getTransports")]
        public ActionResult<IEnumerable<Transport>> GetTransports()
        {
            return _context.Transports.ToList();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putTransport")]
        public IActionResult PutTour([FromBody] Transport newTransport)
        {
            try
            {
                Transport? transport = _context.Transports.First(x => x.Id == newTransport.Id);

                if (transport is null)
                    return NotFound();

                transport.Name = newTransport.Name;
                transport.Tours = _context.Tours.Where(x => x.TransportId == newTransport.Id).ToList();

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
        [Route("/deleteTransport")]
        public IActionResult DeleteTransport(int id)
        {
            try
            {
                Transport? transport = _context.Transports.First(x => x.Id == id);
                if (transport is null)
                    return NotFound();

                _context.Transports.Remove(transport);
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
