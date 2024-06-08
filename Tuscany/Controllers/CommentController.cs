using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.Models;
using Tuscany.WebModels;

namespace Tuscany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[Authorize]
        [HttpPost]
        [Route("/postComment")]
        public IActionResult PostComment([FromBody] CommentWeb commentWeb)
        {
            try
            {
                Comment comment = new()
                {
                    Text = commentWeb.Text,
                    UserId = commentWeb.UserId,
                    TourId = commentWeb.TourId
                };
                _unitOfWork.Comment.Add(comment);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        //[Authorize]
        [HttpGet]
        [Route("/getComments")]
        public ActionResult<IEnumerable<CommentWeb>> GetComments()
        {
            List<Comment> comments = _unitOfWork.Comment.GetAll().ToList();
            return comments.Select(x => new CommentWeb
            {
                Id = x.Id,
                Text = x.Text,
                UserId = x.UserId,
                TourId = x.TourId
            }).ToList();
        }

        //[Authorize]
        [HttpPut]
        [Route("/putComment")]
        public IActionResult PutComment([FromBody] CommentWeb newOrder)
        {
            try
            {
                Comment comment = new()
                {
                    Id = newOrder.Id,
                    Text = newOrder.Text,
                    UserId = newOrder.UserId,
                    TourId = newOrder.TourId
                };

                _unitOfWork.Comment.Update(comment);
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
        [Route("/deleteComment")]
        public IActionResult DeleteCommet(int id)
        {
            try
            {
                Comment? comment = _unitOfWork.Comment.Get(x => x.Id == id);
                if (comment is null)
                    return NotFound();

                _unitOfWork.Comment.Remove(comment);
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
