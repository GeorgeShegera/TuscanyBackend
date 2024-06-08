using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuscanyBackend.Classes;
using TuscanyBackend.DB;
using TuscanyBackend.DB.Models;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToursLanguageController : Controller
    {
        private readonly DbTuscanyContext _context;

        public ToursLanguageController()
        {
            _context = new DbTuscanyContext();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postToursLanguages")]
        public IActionResult PostToursLanguages([FromBody] ToursLanguage toursLanguage)
        {
            try
            {
                toursLanguage.Tour = _context.Tours.FirstOrDefault(x => x.Id == toursLanguage.TourId);
                toursLanguage.Language = _context.Languages.FirstOrDefault(x => x.Id == toursLanguage.LanguageId);
                _context.ToursLanguages.Add(toursLanguage);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        [HttpGet]
        [Route("/getToursLanguages")]
        public ActionResult<IEnumerable<ToursLanguage>> GetTourSchedules()
        {
            return _context.ToursLanguages.ToList();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putToursLanguage")]
        public IActionResult PutTour([FromBody] ToursLanguage newTourLanguage)
        {
            try
            {
                ToursLanguage? tourLanguage = _context.ToursLanguages.First(x => x.Id == newTourLanguage.Id);

                if (tourLanguage is null)
                    return NotFound();

                tourLanguage.LanguageId = newTourLanguage.LanguageId;
                tourLanguage.TourId = newTourLanguage.TourId;
                tourLanguage.Language = _context.Languages.First(x => x.Id == newTourLanguage.LanguageId);
                tourLanguage.Tour = _context.Tours.First(x => x.Id == newTourLanguage.TourId);

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
        [Route("/deleteToursLanguages")]
        public IActionResult DeleteToursLanguages(int id)
        {
            try
            {
                ToursLanguage? tourLanguage = _context.ToursLanguages.First(x => x.Id == id);
                if (tourLanguage is null)
                    return NotFound();

                _context.ToursLanguages.Remove(tourLanguage);
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
