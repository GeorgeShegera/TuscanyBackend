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
        private readonly ConvertUtility convertUtility;
        public TourController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            convertUtility = new ConvertUtility(unitOfWork);
        }

        [Authorize(Roles = UserRoles.Admin)]
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
                    MinNumberOfGroup = tourWeb.MinNumberOfGroup ?? 0,
                    MaxNumberOfGroup = tourWeb.MaxNumberOfGroup ?? 0,
                    TransportId = tourWeb.TransportId ?? 1,
                    Duration = tourWeb.Duration ?? 0,
                    GuideService = tourWeb.GuideService ?? false,
                    DepAndArrivAreas = tourWeb.DepAndArrivAreas ?? "",
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
                    .GroupJoin(_unitOfWork.ToursScheduleType.GetAll(),
                          ts => ts.ScheduleTypeId,
                          tst => tst.Id,
                          (ts, tst) => new
                          {
                              ts.TourId,
                              ts.DateTime,
                              ts.Id,
                              tst
                          })
                    .ToList()
                    .SelectMany(x => x.tst.DefaultIfEmpty(),
                    (groupedObj, schedType) => new
                    {
                        groupedObj.Id,
                        groupedObj.TourId,
                        groupedObj.DateTime,
                        schedType?.ScheduleType
                    })
                    .ToList()
                    .Where(res => res.TourId == tourId &&
                           ((res.ScheduleType is null && res.DateTime > DateTime.Now) ||
                            res.ScheduleType == "at your choice" ||
                            res.ScheduleType == "monday" ||
                            res.ScheduleType == "friday"
                            ))
                    .Select(x => new ScheduleDateTypeMod(x.Id, x.DateTime, x.ScheduleType))
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("/getPopularTourCards/{top}")]
        public ActionResult<IEnumerable<TourCard>> GetToursCard(int top)
        {
            List<Tour> tours = _unitOfWork.Tour
                .GetAllToursWithComments()
                .OrderByDescending(t => t.Comments.Count)
                .Take(top)
                .ToList();

            return convertUtility.ConvertTourCards(tours).ToList();
        }

        [HttpGet]
        [Route("/getTourCards")]
        public ActionResult<IEnumerable<TourCard>> GetTourCards([FromQuery] TourCardsDto tourCardsDto)
        {
            IEnumerable<Tour> tours = _unitOfWork.Tour
                .GetAll()
                .Skip(tourCardsDto.PagPage * tourCardsDto.CardsCount)
                .Take(tourCardsDto.CardsCount);

            tours = tours.Where(x => tourCardsDto.SearchName is null || x.Name.Contains(tourCardsDto.SearchName, StringComparison.OrdinalIgnoreCase));
            return convertUtility.ConvertTourCards(tours).ToList();
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putTour")]
        public IActionResult PutTour([FromBody] TourWeb tourWeb)
        {
            try
            {
                // Fetch the existing tour from the database
                var existingTour = _unitOfWork.Tour.Get(x => x.Id == tourWeb.Id);

                if (existingTour == null)
                {
                    return NotFound("Tour not found");
                }

                // Update only the provided properties
                if (!string.IsNullOrWhiteSpace(tourWeb.Name))
                {
                    existingTour.Name = tourWeb.Name;
                }
                if (!string.IsNullOrWhiteSpace(tourWeb.Details))
                {
                    existingTour.Details = tourWeb.Details;
                }
                if (!string.IsNullOrWhiteSpace(tourWeb.Description))
                {
                    existingTour.Description = tourWeb.Description;
                }
                if (tourWeb.MinNumberOfGroup.HasValue)
                {
                    existingTour.MinNumberOfGroup = tourWeb.MinNumberOfGroup.Value;
                }
                if (tourWeb.MaxNumberOfGroup.HasValue)
                {
                    existingTour.MaxNumberOfGroup = tourWeb.MaxNumberOfGroup.Value;
                }
                if (tourWeb.TransportId.HasValue)
                {
                    existingTour.TransportId = tourWeb.TransportId.Value;
                }
                if (tourWeb.Duration.HasValue)
                {
                    existingTour.Duration = tourWeb.Duration.Value;
                }
                if (tourWeb.GuideService.HasValue)
                {
                    existingTour.GuideService = tourWeb.GuideService.Value;
                }
                if (!string.IsNullOrWhiteSpace(tourWeb.DepAndArrivAreas))
                {
                    existingTour.DepAndArrivAreas = tourWeb.DepAndArrivAreas;
                }
                if (tourWeb.EntryFees.HasValue)
                {
                    existingTour.EntryFees = tourWeb.EntryFees;
                }
                if (tourWeb.AdultPrice.HasValue)
                {
                    existingTour.AdultPrice = tourWeb.AdultPrice.Value;
                }
                if (tourWeb.ChildPrice.HasValue)
                {
                    existingTour.ChildPrice = tourWeb.ChildPrice.Value;
                }

                // Save the updated entity
                _unitOfWork.Tour.Update(existingTour);
                _unitOfWork.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Roles = UserRoles.Admin)]
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
