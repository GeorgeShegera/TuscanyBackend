using Microsoft.AspNetCore.Mvc;
using TuscanyBackend.DB.Models;
using TuscanyBackend.DB;
using Microsoft.AspNetCore.Authorization;
using TuscanyBackend.Classes;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : Controller
    {
        private readonly DbTuscanyContext _context;
        public TicketController()
        {
            _context = new DbTuscanyContext();
        }

        [Authorize]
        [HttpPost]
        [Route("/postTicket")]
        public IActionResult PostTicket([FromBody] Ticket ticket)
        {
            try
            {
                ticket.Type = _context.TicketsTypes.FirstOrDefault(x => x.Id == ticket.TypeId);
                ticket.Order = _context.Orders.FirstOrDefault(x => x.Id == ticket.OrderId);
                _context.Tickets.Add(ticket);
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
        [Route("/getTickets")]
        public ActionResult<IEnumerable<Ticket>> GetTickets()
        {
            return _context.Tickets.ToList();
        }

        [Authorize]
        [HttpPut]
        [Route("/putTicket")]
        public IActionResult PutTicket([FromBody] Ticket newTicket)
        {
            try
            {
                Ticket? ticket = _context.Tickets.First(x => x.Id == newTicket.Id);

                if (ticket == null)
                    return NotFound();

                ticket.Price = newTicket.Price;
                ticket.OrderId = newTicket.OrderId;
                ticket.TypeId = newTicket.TypeId;
                ticket.Type = _context.TicketsTypes.First(x => x.Id == ticket.TypeId);
                ticket.Order = _context.Orders.First(x => x.Id == ticket.OrderId);

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
        [Route("/deleteTicket")]
        public IActionResult DeleteTicket(int id)
        {
            try
            {
                Ticket? ticket = _context.Tickets.First(x => x.Id == id) ?? throw new Exception();
                _context.Tickets.Remove(ticket);
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
