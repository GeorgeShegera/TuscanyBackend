using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tuscany.DataAccess.DB;
using Tuscany.DataAccess.Repository;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.Models;
using Tuscany.Utility;
using Tuscany.WebModels;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LanguagesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LanguagesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postLanguage")]
        public IActionResult PostLanguage([FromBody] LanguageWeb languageWeb)
        {
            try
            {
                Language language = new()
                {
                    Name = languageWeb.Name
                };
                _unitOfWork.Language.Add(language);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        [HttpGet]
        [Route("/getLanguages")]
        public ActionResult<IEnumerable<LanguageWeb>> getLanguages()
        {
            List<Language> languages = _unitOfWork.Language.GetAll().ToList();
            return languages.Select(x => new LanguageWeb
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }


        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putLanguage")]
        public IActionResult PutLanguage([FromBody] LanguageWeb newLanguage)
        {
            try
            {
                Language language = new()
                {
                    Id = newLanguage.Id,
                    Name = newLanguage.Name
                };

                _unitOfWork.Language.Update(language);
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
        [Route("/deleteLanguage")]
        public IActionResult DeleteLanguage(int id)
        {
            try
            {
                Language? language = _unitOfWork.Language.Get(x => x.Id == id);
                if (language is null)
                    return NotFound();

                _unitOfWork.Language.Remove(language);
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
