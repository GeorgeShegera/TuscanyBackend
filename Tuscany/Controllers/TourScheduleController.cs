using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tuscany.DataAccess.DB;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.Models;
using Tuscany.Utility;
using Tuscany.WebModels;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TourScheduleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TourScheduleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postTourSchedule")]
        public IActionResult PostTourSchedule([FromBody] TourScheduleWeb toursScheduleWeb)
        {
            try
            {
                ToursSchedule toursSchedule = new()
                {
                    DateTime = toursScheduleWeb.DateTime,
                    TourId = toursScheduleWeb.TourId,
                    ScheduleTypeId = toursScheduleWeb.ScheduleTypeId,
                };
                _unitOfWork.ToursSchedule.Add(toursSchedule);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/getTourSchedules")]
        public ActionResult<IEnumerable<TourScheduleWeb>> GetTourSchedules()
        {
            List<ToursSchedule> toursSchedules = _unitOfWork.ToursSchedule.GetAll().ToList();
            return toursSchedules.Select(x => new TourScheduleWeb
            {
                Id = x.Id,
                DateTime = x.DateTime,
                TourId = x.TourId,
                ScheduleTypeId = x.ScheduleTypeId,
            }).ToList();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putTourSchedule")]
        public IActionResult PutTour([FromBody] TourScheduleWeb tourScheduleWeb)
        {
            try
            {
                ToursSchedule schedule = new()
                {
                    Id = tourScheduleWeb.Id,
                    DateTime = tourScheduleWeb.DateTime,
                    TourId = tourScheduleWeb.TourId,
                    ScheduleTypeId = tourScheduleWeb.ScheduleTypeId,
                };

                _unitOfWork.ToursSchedule.Update(schedule);
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
        [Route("/deleteTourSchedule")]
        public IActionResult DeleteTour(int id)
        {
            try
            {
                ToursSchedule? tourSchedule = _unitOfWork.ToursSchedule.Get(x => x.Id == id);

                if (tourSchedule is null)
                    return NotFound();

                _unitOfWork.ToursSchedule.Remove(tourSchedule);
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
