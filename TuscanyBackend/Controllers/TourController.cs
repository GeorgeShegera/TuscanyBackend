using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Runtime.Serialization;
using TuscanyBackend.Classes;
using TuscanyBackend.DB;
using TuscanyBackend.DB.Models;
using TuscanyBackend.WebModels;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TourController : Controller
    {
        private readonly DbTuscanyContext _context;
        public TourController()
        {
            _context = new DbTuscanyContext();
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postTour")]
        public IActionResult PostTour([FromBody] Tour tour)
        {
            try
            {
                tour.Galleries = _context.Galleries.Where(x => x.TourId == tour.Id).ToList();
                tour.ToursLanguages = _context.ToursLanguages.Where(x => x.TourId == tour.Id).ToList();
                tour.ToursSchedules = _context.ToursSchedules.Where(x => x.TourId == tour.Id).ToList();
                tour.Transport = _context.Transports.FirstOrDefault(x => x.Id == tour.TransportId)!;

                _context.Tours.Add(tour);
                _context.SaveChanges();
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
            Tour? tour = _context.Tours.FirstOrDefault(x => x.Id == tourId);
            List<TourScheduleWebMod>? dates = _context.ToursSchedules
                .Join(_context.ToursScheduleTypes,
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
                .Select(x => new TourScheduleWebMod(x.DateTime, x.ScheduleType))
                .ToList();

            List<string>? languages = (from tourLang in _context.ToursLanguages
                                       where tourLang.TourId == tourId
                                       from lang in _context.Languages
                                       where lang.Id == tourLang.LanguageId
                                       select lang.LanguageName).ToList();

            string? transport = _context.Transports.FirstOrDefault(x => x.Id == tour!.TransportId)?.Name;
            List<string>? imgs = _context.Galleries.Where(x => x.TourId == tourId).Select(x => x.Img).ToList()!;


            return new TourPage(tour, dates, languages, transport, imgs);
        }

        [HttpGet]
        [Route("/getTourCards")]
        public ActionResult<IEnumerable<TourCard>> GetToursCard()
        {
            List<TourCard> cards = new();
            List<Tour> tours = _context.Tours.ToList();
            foreach (var tour in tours)
            {
                ToursSchedule schedule = _context.ToursSchedules.FirstOrDefault(x => x.TourId == tour.Id)!;
                ToursScheduleType scheduleType = _context.ToursScheduleTypes.FirstOrDefault(x => x.Id == schedule.ScheduleTypeId)!;
                string? imgUrl = _context.Galleries.FirstOrDefault(x => x.TourId == tour.Id)?.Img;
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


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putTour")]
        public IActionResult PutTour([FromBody] Tour newTour)
        {
            try
            {
                Tour? tour = _context.Tours.First(x => x.Id == newTour.Id);

                if (tour == null)
                    return NotFound();

                tour.Name = newTour.Name;
                tour.Details = newTour.Details;
                tour.Description = newTour.Description;
                tour.MinNumberOfGroup = newTour.MinNumberOfGroup;
                tour.MaxNumberOfGroup = newTour.MaxNumberOfGroup;
                tour.TransportId = newTour.TransportId;
                tour.Duration = newTour.Duration;
                tour.GuideService = newTour.GuideService;
                tour.DepAndArrivAreas = newTour.DepAndArrivAreas;
                tour.EntryFees = newTour.EntryFees;
                tour.AdultPrice = newTour.AdultPrice;
                tour.ChildPrice = newTour.ChildPrice;

                tour.Galleries = _context.Galleries.Where(x => x.TourId == newTour.Id).ToList();
                tour.ToursLanguages = _context.ToursLanguages.Where(x => x.TourId == newTour.Id).ToList();
                tour.ToursSchedules = _context.ToursSchedules.Where(x => x.TourId == newTour.Id).ToList();
                tour.Transport = _context.Transports.First(x => x.Id == newTour.TransportId);


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
        [Route("/deleteTour")]
        public IActionResult DeleteTour(int id)
        {
            Tour? tour = _context.Tours.First(x => x.Id == id);
            if (tour is null)
                return NotFound();

            _context.Tours.Remove(tour);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
