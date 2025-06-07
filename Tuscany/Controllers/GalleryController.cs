using Microsoft.AspNetCore.Mvc;
using Tuscany.DataAccess.DB;
using Tuscany.Models;
using Tuscany.Utility;
using Microsoft.AspNetCore.Authorization;
using Tuscany.DataAccess.Repository;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.WebModels;
using System.Linq.Expressions;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GalleryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public GalleryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postGallery")]
        public IActionResult PostTourGallery([FromBody] GalleryWeb galleryWebModel)
        {
            try
            {
                Gallery gallery = new()
                {
                    Img = galleryWebModel.Img,
                    TourId = galleryWebModel.TourId
                };
                _unitOfWork.Gallery.Add(gallery);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        [HttpGet]
        [Route("/getGalleries")]
        public ActionResult<IEnumerable<GalleryWeb>> GetGallerys()
        {
            var galleries = _unitOfWork.Gallery.GetAll().ToList();
            var galleryModels = galleries.Select(x => new GalleryWeb()
            {
                Id = x.Id,
                Img = x.Img,
                TourId = x.TourId,
            }).ToList();

            return galleryModels;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putGallery")]
        public IActionResult PutGallery([FromBody] GalleryWeb galleryWeb)
        {
            try
            {
                Gallery gallery = new()
                {
                    Id = galleryWeb.Id,
                    Img = galleryWeb.Img,
                    TourId = galleryWeb.TourId
                };
                _unitOfWork.Gallery.Update(gallery);
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
        [Route("/deleteGallery")]
        public IActionResult DeleteGallery(int id)
        {
            try
            {
                Gallery? gallery = _unitOfWork.Gallery.Get(x => x.Id == id);
                if (gallery is null)
                    return NotFound();

                _unitOfWork.Gallery.Remove(gallery);
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
