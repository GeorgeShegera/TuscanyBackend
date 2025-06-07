using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Tuscany.Models;
using Tuscany.Utility;
using Tuscany.WebModels;
using Tuscany.DataAccess.Repository.IRepository;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TicketController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpPost]
        [Route("/postTicket")]
        public IActionResult PostTicket([FromBody] TicketWeb ticketWeb)
        {
            try
            {
                Ticket ticket = new()
                {
                    Price = ticketWeb.Price,
                    OrderId = ticketWeb.OrderId,
                    TypeId = ticketWeb.TypeId,
                };
                _unitOfWork.Ticket.Add(ticket);
                _unitOfWork.Save();
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
        public ActionResult<IEnumerable<TicketWeb>> GetTickets()
        {
            List<Ticket> tickets = _unitOfWork.Ticket.GetAll().ToList();
            return tickets.Select(x => new TicketWeb()
            {
                Id = x.Id,
                Price = x.Price,
                OrderId = x.OrderId,
                TypeId = x.TypeId,
            }).ToList();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putTicket")]
        public IActionResult PutTicket([FromBody] TicketWeb ticketWeb)
        {
            try
            {
                Ticket? ticket = new()
                {
                    Id = ticketWeb.Id,
                    Price = ticketWeb.Price,
                    OrderId = ticketWeb.OrderId,
                    TypeId = ticketWeb.TypeId,
                };

                _unitOfWork.Ticket.Update(ticket);
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
        [Route("/deleteTicket")]
        public IActionResult DeleteTicket(int id)
        {
            try
            {
                Ticket? ticket = _unitOfWork.Ticket.Get(x => x.Id == id);

                if (ticket == null)
                    return NotFound();

                _unitOfWork.Ticket.Remove(ticket);
                _unitOfWork.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }
    }
}
