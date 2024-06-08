using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Runtime.Serialization;
using Tuscany.DataAccess.DB;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.Models;
using Tuscany.Utility;
using Tuscany.WebModels;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TourController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TourController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postTour")]
        public IActionResult PostTour([FromBody] TourWeb tourWeb)
        {
            try
            {
                Tour tour = new()
                {
                    Name = tourWeb.Name,
                    Details = tourWeb.Details,
                    Description = tourWeb.Description,
                    MinNumberOfGroup = tourWeb.MinNumberOfGroup,
                    MaxNumberOfGroup = tourWeb.MaxNumberOfGroup,
                    TransportId = tourWeb.TransportId,
                    Duration = tourWeb.Duration,
                    GuideService = tourWeb.GuideService,
                    DepAndArrivAreas = tourWeb.DepAndArrivAreas,
                    EntryFees = tourWeb.EntryFees,
                    AdultPrice = tourWeb.AdultPrice,
                    ChildPrice = tourWeb.ChildPrice,
                };
                _unitOfWork.Tour.Add(tour);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        [HttpGet]
        [Route("/getTourWebModel")]
        public ActionResult<TourPage> GetTourWebModel(int tourId)
        {
            try
            {

            
            Tour? tour = _unitOfWork.Tour.Get(x => x.Id == tourId);
            List<ScheduleDateTypeMod>? dates = _unitOfWork.ToursSchedule.GetAll()
                .Join(_unitOfWork.ToursScheduleType.GetAll(),
                      ts => ts.ScheduleTypeId,
                      tst => tst.Id,
                      (ts, tst) => new
                      {
                          ts.TourId,
                          ts.DateTime,
                          tst.ScheduleType
                      })
                .ToList()
                .Where(res => res.TourId == tourId &&
                       (res.ScheduleType != "at your choice" ||
                        res.ScheduleType != "monday" ||
                        res.ScheduleType != "friday" ||
                        res.DateTime > DateTime.Now))
                .Select(x => new ScheduleDateTypeMod(x.DateTime, x.ScheduleType))
                .ToList();

            List<string>? languages = (from tourLang in _unitOfWork.ToursLanguage.GetAll()
                                       where tourLang.TourId == tourId
                                       from lang in _unitOfWork.Language.GetAll()
                                       where lang.Id == tourLang.LanguageId
                                       select lang.Name).ToList();

            string? transport = _unitOfWork.Transport.Get(x => x.Id == tour!.TransportId)?.Name;
            List<string>? imgs = _unitOfWork.Gallery.GetAll().Where(x => x.TourId == tourId).Select(x => x.Img).ToList()!;
            

            return new TourPage(tour, dates, languages, transport, imgs);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("/getTourCards")]
        public ActionResult<IEnumerable<TourCard>> GetToursCard()
        {
            List<TourCard> cards = [];
            List<Tour> tours = _unitOfWork.Tour.GetAll().ToList();
            foreach (var tour in tours)
            {
                ToursSchedule schedule = _unitOfWork.ToursSchedule.Get(x => x.TourId == tour.Id)!;
                ToursScheduleType scheduleType = _unitOfWork.ToursScheduleType.Get(x => x.Id == schedule.ScheduleTypeId)!;
                string? imgUrl = _unitOfWork.Gallery.Get(x => x.TourId == tour.Id)?.Img;
                cards.Add(new TourCard()
                {
                    Id = tour.Id,
                    Name = tour.Name,
                    Price = tour.AdultPrice,
                    Img = imgUrl,
                    ScheduleType = scheduleType.ScheduleType,
                    MinNumberOfGroup = tour.MinNumberOfGroup,
                    MaxNumberOfGroup = tour.MaxNumberOfGroup,
                    Description = tour.Description,
                });
            }
            return cards;
        }


        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putTour")]
        public IActionResult PutTour([FromBody] TourWeb tourWeb)
        {
            try
            {
                Tour? tour = new()
                {
                    Name = tourWeb.Name,
                    Details = tourWeb.Details,
                    Description = tourWeb.Description,
                    MinNumberOfGroup = tourWeb.MinNumberOfGroup,
                    MaxNumberOfGroup = tourWeb.MaxNumberOfGroup,
                    TransportId = tourWeb.TransportId,
                    Duration = tourWeb.Duration,
                    GuideService = tourWeb.GuideService,
                    DepAndArrivAreas = tourWeb.DepAndArrivAreas,
                    EntryFees = tourWeb.EntryFees,
                    AdultPrice = tourWeb.AdultPrice,
                    ChildPrice = tourWeb.ChildPrice,
                };

                _unitOfWork.Tour.Update(tour);
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
        [Route("/deleteTour")]
        public IActionResult DeleteTour(int id)
        {
            Tour? tour = _unitOfWork.Tour.Get(x => x.Id == id);
            if (tour is null)
                return NotFound();

            _unitOfWork.Tour.Remove(tour);
            _unitOfWork.Save();

            return Ok();
        }
    }
}
