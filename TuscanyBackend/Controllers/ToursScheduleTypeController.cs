using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuscanyBackend.Classes;
using TuscanyBackend.DB;
using TuscanyBackend.DB.Models;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToursScheduleTypeController : Controller
    {
        private readonly DbTuscanyContext _context;

        public ToursScheduleTypeController()
        {
            _context = new DbTuscanyContext();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postTourScheduleType")]
        public IActionResult PostTourScheduleType([FromBody] ToursScheduleType type)
        {
            try
            {
                type.ToursSchedules = _context.ToursSchedules.Where(x => x.ScheduleTypeId == type.Id).ToList(); ;
                _context.ToursScheduleTypes.Add(type);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }
        
        [HttpGet]
        [Route("/getTourScheduleTypes")]
        public ActionResult<IEnumerable<ToursScheduleType>> GetTourScheduleTypes()
        {
            return _context.ToursScheduleTypes.ToList();
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putTourScheduleType")]
        public IActionResult PutTour([FromBody] ToursScheduleType newTourScheduleType)
        {
            try
            {
                ToursScheduleType? tourScheduleType = _context.ToursScheduleTypes.First(x => x.Id == newTourScheduleType.Id);

                if (tourScheduleType is null)
                    return NotFound();

                tourScheduleType.ScheduleType = newTourScheduleType.ScheduleType;
                tourScheduleType.ToursSchedules = _context.ToursSchedules.Where(x => x.ScheduleTypeId == newTourScheduleType.Id).ToList();

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
        [Route("/deleteTourScheduleType")]
        public IActionResult DeleteTour(int id)
        {
            try
            {
                ToursScheduleType? tourScheduleType = _context.ToursScheduleTypes.First(x => x.Id == id);
                if (tourScheduleType is null)
                    return NotFound();

                _context.ToursScheduleTypes.Remove(tourScheduleType);
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
