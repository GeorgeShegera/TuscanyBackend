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
    public class ToursScheduleTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToursScheduleTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postTourScheduleType")]
        public IActionResult PostTourScheduleType([FromBody] ScheduleTypeWeb type)
        {
            try
            {
                ToursScheduleType tourScheduleType = new()
                {
                    ScheduleType = type.ScheduleType
                };
                _unitOfWork.ToursScheduleType.Add(tourScheduleType);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        [HttpGet]
        [Route("/getTourScheduleTypes")]
        public ActionResult<IEnumerable<ScheduleTypeWeb>> GetTourScheduleTypes()
        {
            List<ToursScheduleType> types = _unitOfWork.ToursScheduleType.GetAll().ToList();
            return types.Select(x => new ScheduleTypeWeb()
            {
                Id = x.Id,
                ScheduleType = x.ScheduleType
            }).ToList();
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putTourScheduleType")]
        public IActionResult PutTour([FromBody] ScheduleTypeWeb tourScheduleTypeWeb)
        {
            try
            {
                ToursScheduleType toursScheduleType = new()
                {
                    Id = tourScheduleTypeWeb.Id,
                    ScheduleType = tourScheduleTypeWeb.ScheduleType
                };

                _unitOfWork.ToursScheduleType.Update(toursScheduleType);
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
        [Route("/deleteTourScheduleType")]
        public IActionResult DeleteTour(int id)
        {
            try
            {
                ToursScheduleType? tourScheduleType = _unitOfWork.ToursScheduleType.Get(x => x.Id == id);

                if (tourScheduleType is null)
                    return NotFound();

                _unitOfWork.ToursScheduleType.Remove(tourScheduleType);
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
