using Microsoft.AspNetCore.Mvc;
using TuscanyBackend.DB.Models;
using TuscanyBackend.DB;
using Microsoft.AspNetCore.Authorization;
using TuscanyBackend.Classes;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketTypeController : Controller
    {
        private readonly DbTuscanyContext _context;
        public TicketTypeController()
        {
            _context = new DbTuscanyContext();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postTicketType")]
        public IActionResult PostTicketType([FromBody] TicketsType ticketsType)
        {
            try
            {
                ticketsType.Tickets = _context.Tickets.Where(x => x.TypeId == ticketsType.Id).ToList();
                _context.TicketsTypes.Add(ticketsType);
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
        [Route("/getTicketTypes")]
        public ActionResult<IEnumerable<TicketsType>> GetTickets()
        {
            return _context.TicketsTypes.ToList();
        }

        [Authorize]
        [HttpPut]
        [Route("/putTicketType")]
        public IActionResult PutTicket([FromBody] TicketsType newTicketType)
        {
            try
            {
                TicketsType? ticket = _context.TicketsTypes.First(x => x.Id == newTicketType.Id);

                if (ticket == null)
                    return NotFound();

                ticket.Type = newTicketType.Type;
                ticket.Tickets = _context.Tickets.Where(x => x.TypeId == newTicketType.Id).ToList();

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
        [Route("/deleteTicketType")]
        public IActionResult DeleteTicketType(int id)
        {
            try
            {
                TicketsType? ticketType = _context.TicketsTypes.First(x => x.Id == id) ?? throw new Exception();
                _context.TicketsTypes.Remove(ticketType);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }
    }
}
