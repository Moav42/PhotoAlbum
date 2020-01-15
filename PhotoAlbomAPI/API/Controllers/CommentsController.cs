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

        [HttpGet("post/{postId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CommentModel>>> GetCommentByPost(int postId)
        {
            var modelBLL = await _commentService.GetByPostAsync(postId);
            var models = new List<CommentModel>();

            foreach (var item in modelBLL)
            {
                models.Add(item.Transform());
            }

            if (models.Count == 0)
            {
                return NotFound();
            }

            return Ok(models);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CommentModel>> GetComment(int id)
        {
            var modelBLL = await _commentService.GetCommentsAsync(id);

            if (modelBLL == null)
            {
                return NotFound("Model doesn`t exicte");
            }
            else
            {
                return Ok(modelBLL.Transform());
            }
        }

        [HttpPost]
        public async Task<ActionResult<CommentModel>> PostComment(CommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _commentService.AddCommentAsync(model.Transform());

            return CreatedAtAction(nameof(GetComment), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Moderator")]
        public async Task<ActionResult<CommentModel>> PutComment(int id, CommentModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _commentService.UpdateCommentAsync(model.Transform());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _commentService.GetCommentsAsync(model.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(model);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            var model = await _commentService.GetCommentsAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            await _commentService.DeleteCommentAsync(model.Id);

            return NoContent();
        }
    }
}