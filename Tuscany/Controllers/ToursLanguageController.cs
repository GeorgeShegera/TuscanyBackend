using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Tuscany.WebModels;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.Models;
using Tuscany.Utility;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToursLanguageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToursLanguageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postToursLanguages")]
        public IActionResult PostToursLanguages([FromBody] ToursLanguageWeb toursLanguageWeb)
        {
            try
            {
                ToursLanguage toursLanguage = new()
                {
                    LanguageId = toursLanguageWeb.LanguageId,
                    TourId = toursLanguageWeb.TourId
                };

                _unitOfWork.ToursLanguage.Add(toursLanguage);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        [HttpGet]
        [Route("/getToursLanguages")]
        public ActionResult<IEnumerable<ToursLanguageWeb>> GetTourSchedules()
        {
            List<ToursLanguage> toursLanguages = _unitOfWork.ToursLanguage.GetAll().ToList();
            return toursLanguages.Select(x => new ToursLanguageWeb()
            {
                Id = x.Id,
                LanguageId = x.LanguageId,
                TourId = x.TourId
            }).ToList();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putToursLanguage")]
        public IActionResult PutTour([FromBody] ToursLanguageWeb tourLanguageWeb)
        {
            try
            {
                ToursLanguage tourLanguage = new()
                {
                    Id = tourLanguageWeb.Id,
                    LanguageId = tourLanguageWeb.LanguageId,
                    TourId = tourLanguageWeb.TourId
                };


                _unitOfWork.ToursLanguage.Update(tourLanguage);
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
        [Route("/deleteToursLanguages")]
        public IActionResult DeleteToursLanguages(int id)
        {
            try
            {
                ToursLanguage? tourLanguage = _unitOfWork.ToursLanguage.Get(x => x.Id == id);
                if (tourLanguage is null)
                    return NotFound();

                _unitOfWork.ToursLanguage.Remove(tourLanguage);
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
