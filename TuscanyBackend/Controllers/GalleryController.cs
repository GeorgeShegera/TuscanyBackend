using Microsoft.AspNetCore.Mvc;
using TuscanyBackend.DB.Models;
using TuscanyBackend.DB;
using Microsoft.AspNetCore.Authorization;
using TuscanyBackend.Classes;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GalleryController : Controller
    {
        private readonly DbTuscanyContext _context;
        public GalleryController()
        {
            _context = new DbTuscanyContext();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("/postGallery")]
        public IActionResult PostTourGallery([FromBody] Gallery gallery)
        {
            try
            {
                gallery.Tour = _context.Tours.First(x => x.Id == gallery.TourId);
                _context.Galleries.Add(gallery);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("/getGalleries")]
        public ActionResult<IEnumerable<Gallery>> GetGallerys()
        {
            return _context.Galleries.ToList();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        [Route("/putGallery")]
        public IActionResult PutGallery([FromBody] Gallery newGallery)
        {
            try
            {
                Gallery? gallery = _context.Galleries.FirstOrDefault(x => x.Id == newGallery.Id);
                if (gallery is null)
                    return NotFound();

                gallery.Img = newGallery.Img;
                gallery.TourId = newGallery.TourId;
                gallery.Tour = _context.Tours.First(x => x.Id == newGallery.TourId);

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
        [Route("/deleteGallery")]
        public IActionResult DeleteGallery(int id)
        {
            try
            {
                Gallery? gallery = _context.Galleries.FirstOrDefault(x => x.Id == id);
                if (gallery is null)
                    return NotFound();

                _context.Galleries.Remove(gallery);
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
