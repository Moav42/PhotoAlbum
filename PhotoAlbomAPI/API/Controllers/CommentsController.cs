using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using API.Models;
using BLL.Models;
using API.Extensions;
using BLL.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService<CommentBLL> _commentService;
        public CommentsController(ICommentService<CommentBLL> commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("post/postId")]
        public async Task<ActionResult<IEnumerable<CommentModel>>> GetCommentByPost(int postId)
        {
            var modelBLL = await _commentService.GetByPostAsync(postId);
            var models = new List<CommentModel>();

            foreach (var item in modelBLL)
            {
                models.Add(item.Transform());
            }
            return models;
        }

        [HttpGet("user/userId")]
        public async Task<ActionResult<IEnumerable<CommentModel>>> GetCommentByUser(string userId)
        {
            var modelBLL = await _commentService.GetByUserAsync(userId);
            var models = new List<CommentModel>();

            foreach (var item in modelBLL)
            {
                models.Add(item.Transform());
            }
            return models;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentModel>> GetComment(int id)
        {
            var modelBLL = await _commentService.GetAsync(id);

            if (modelBLL == null)
            {
                return BadRequest("Model doesn`t exicte");
            }
            else
            {
                return modelBLL.Transform();
            }
        }

        [HttpPost]
        public async Task<ActionResult<CommentModel>> PostComment(CommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            await _commentService.AddAsync(model.Transform());

            return CreatedAtAction("GetCategory", new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CommentModel>> PutComment(CommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            if (_commentService.GetAsync(model.Id) == null)
            {
                return BadRequest("Model doesn`t exicte");
            }
            await _commentService.UpdateAsync(model.Transform());
            return model;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CommentModel>> DeleteComment(int id)
        {
            var model = await _commentService.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            await _commentService.DeleteAsync(model.Id);

            return model.Transform();
        }
    }
}