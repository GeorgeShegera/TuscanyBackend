using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Tuscany.WebModels;
using Tuscany.Models;
using Tuscany.Utility;
using Tuscany.DataAccess.Repository.IRepository;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TicketTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postTicketType")]
        public IActionResult PostTicketType([FromBody] TicketTypeWeb ticketsTypeWeb)
        {
            try
            {
                TicketsType ticketsType = new()
                {
                    Type = ticketsTypeWeb.Type
                };

                _unitOfWork.TicketType.Add(ticketsType);
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
        [Route("/getTicketTypes")]
        public ActionResult<IEnumerable<TicketTypeWeb>> GetTickets()
        {
            List<TicketsType> tickets = _unitOfWork.TicketType.GetAll().ToList();
            return tickets.Select(x => new TicketTypeWeb
            {
                Type = x.Type,
            }).ToList();
        }

        //[Authorize]
        [HttpPut]
        [Route("/putTicketType")]
        public IActionResult PutTicket([FromBody] TicketTypeWeb newTicketType)
        {
            try
            {
                TicketsType? ticketType = new()
                {
                    Id = newTicketType.Id,
                    Type = newTicketType.Type
                };

                _unitOfWork.TicketType.Update(ticketType);
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
        [Route("/deleteTicketType")]
        public IActionResult DeleteTicketType(int id)
        {
            try
            {
                TicketsType? ticketType = _unitOfWork.TicketType.Get(x => x.Id == id) ?? throw new Exception();

                if (ticketType == null)
                    return NotFound();

                _unitOfWork.TicketType.Remove(ticketType);
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
