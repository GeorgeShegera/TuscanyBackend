using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuscanyBackend.Classes;
using TuscanyBackend.DB;
using TuscanyBackend.DB.Models;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LanguagesController : Controller
    {
        private readonly DbTuscanyContext _context;
        public LanguagesController()
        {
            _context = new DbTuscanyContext();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postLanguage")]
        public IActionResult PostLanguage([FromBody] Language language)
        {
            try
            {
                language.ToursLanguages = _context.ToursLanguages.Where(x => x.LanguageId == language.Id).ToList();
                _context.Languages.Add(language);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        [HttpGet]
        [Route("/getLanguages")]
        public ActionResult<IEnumerable<Language>> getLanguages()
        {
            return _context.Languages.ToList();
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putLanguage")]
        public IActionResult PutLanguage([FromBody] Language newLanguage)
        {
            try
            {
                Language? language = _context.Languages.First(x => x.Id == newLanguage.Id);

                if (language is null)
                    return NotFound();

                language.LanguageName = newLanguage.LanguageName;
                language.ToursLanguages = _context.ToursLanguages.Where(x => x.LanguageId == newLanguage.Id).ToList();


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
        [Route("/deleteLanguage")]
        public IActionResult DeleteLanguage(int id)
        {
            try
            {
                Language? language = _context.Languages.First(x => x.Id == id);
                if (language is null)
                    return NotFound();

                _context.Languages.Remove(language);
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
