using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;
using TuscanyBackend.DB;
using TuscanyBackend.DB.Models;

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


        [HttpPost]
        [Route("/postTour")]
        public IActionResult PostTour([FromBody] Tour tour)
        {
            try
            {
                _context.Tours.Add(tour);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/getTours")]
        public ActionResult<IEnumerable<Tour>> GetTours()
        {
            return _context.Tours.ToList();
        }


        [HttpPut]
        [Route("/putTour")]
        public IActionResult PutTour(Tour tour)
        {
            _context.Entry(tour).State = EntityState.Modified;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tours.Any(x => x.Id == tour.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete]
        [Route("/deleteTour")]
        public IActionResult DeleteTour(int id)
        {
            Tour? tour = _context.Tours.Find(id);
            if (tour is null)
                return NotFound();

            _context.Tours.Remove(tour);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
