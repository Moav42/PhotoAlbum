using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using BLL.Models;
using API.Extensions;
using BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AllUsers")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService<CommentBLL> _commentService;

        public CommentsController(ICommentService<CommentBLL> commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("post/postId")]
        [AllowAnonymous]
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
        [AllowAnonymous]
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

            return new OkObjectResult(model);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Moderator")]
        public async Task<ActionResult<CommentModel>> PutComment(CommentModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _commentService.UpdateAsync(model.Transform());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _commentService.GetAsync(model.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return new OkObjectResult(model);
            }
            return BadRequest("Not a valid model");
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

            return new OkObjectResult(model.Transform());
        }
    }
}