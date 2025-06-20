﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.Models;
using Tuscany.Utility;
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

        [Authorize]
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

        [HttpGet]
        [Route("/getComment")]
        public ActionResult<List<CommentWebUser>> GetComments(int? tourId)
        {
            var comments = from c in _unitOfWork.Comment.GetAll()
                           join u in _unitOfWork.User.GetAll()
                           on c.UserId.ToString() equals u.Id.ToString()
                           select new CommentWebUser()
                           {
                               Id = c.Id,
                               Text = c.Text,
                               UserId = c.UserId,
                               TourId = c.TourId,
                               Name = u.Name,
                               Surname = u.Surname,
                               AvatarUrl = u.Avatar
                           };

            if (tourId is null)
            {
                comments = (from c in comments
                            orderby Guid.NewGuid()
                            select c)
                            .Take(5);
            }
            else
            {
                comments = from c in comments
                           where c.TourId == tourId
                           select c;
            }

            return comments.ToList();
        }

        [Authorize]
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


        [Authorize(Roles = UserRoles.Admin)]
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
