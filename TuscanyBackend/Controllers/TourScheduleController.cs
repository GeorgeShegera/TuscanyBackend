using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuscanyBackend.Classes;
using TuscanyBackend.DB;
using TuscanyBackend.DB.Models;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TourScheduleController : Controller
    {
        private readonly DbTuscanyContext _context;
        public TourScheduleController()
        {
            _context = new DbTuscanyContext();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postTourSchedule")]
        public IActionResult PostTourSchedule([FromBody] ToursSchedule toursSchedule)
        {
            try
            {
                toursSchedule.Orders = _context.Orders.Where(x => x.TourSchedule == toursSchedule.Id).ToList();
                toursSchedule.Tour = _context.Tours.FirstOrDefault(x => x.Id == toursSchedule.TourId);
                toursSchedule.ScheduleType = _context.ToursScheduleTypes.FirstOrDefault(x => x.Id == toursSchedule.ScheduleTypeId);
                _context.ToursSchedules.Add(toursSchedule);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        [Route("/getTourSchedules")]
        public ActionResult<IEnumerable<ToursSchedule>> GetTourSchedules()
        {
            return _context.ToursSchedules.ToList();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putTourSchedule")]
        public IActionResult PutTour([FromBody] ToursSchedule newSchedule)
        {
            try
            {
                ToursSchedule? schedule = _context.ToursSchedules.First(x => x.Id == newSchedule.Id);

                if (schedule is null)
                    return NotFound();

                schedule.DateTime = newSchedule.DateTime;
                schedule.TourId = newSchedule.TourId;
                schedule.ScheduleTypeId = newSchedule.ScheduleTypeId;
                schedule.Orders = _context.Orders.Where(x => x.TourSchedule == newSchedule.Id).ToList();
                schedule.Tour = _context.Tours.First(x => x.Id == newSchedule.TourId);
                schedule.ScheduleType = _context.ToursScheduleTypes.First(x => x.Id == newSchedule.ScheduleTypeId);

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
        [Route("/deleteTourSchedule")]
        public IActionResult DeleteTour(int id)
        {
            try
            {
                ToursSchedule? tourSchedule = _context.ToursSchedules.First(x => x.Id == id);
                if (tourSchedule is null)
                    return NotFound();

                _context.ToursSchedules.Remove(tourSchedule);
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
