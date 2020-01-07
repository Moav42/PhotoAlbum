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
    /// <summary>
    /// A controller representing functionality to manage the corresponding resource
    /// </summary>
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

        /// <summary>
        /// Gets all commenst of given post
        /// </summary>
        /// <returns>If result success returns comments, if it's not return NotFound</returns>
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

        /// <summary>
        /// Gets comment by id
        /// </summary>
        /// <returns>If result success returns comment, if it's not return NotFound</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CommentModel>> GetComment(int id)
        {
            var modelBLL = await _commentService.GetAsync(id);

            if (modelBLL == null)
            {
                return NotFound("Model doesn`t exicte");
            }
            else
            {
                return Ok(modelBLL.Transform());
            }
        }

        /// <summary>
        /// Creates new comment
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// If the provided model is not valid returns a BadRequest with the state of the model, 
        /// If the result is successful, returns the created Created status code with the model
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<CommentModel>> PostComment(CommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _commentService.AddAsync(model.Transform());

            return CreatedAtAction(nameof(GetComment), new { id = model.Id }, model);
        }

        /// <summary>
        /// Edits comments
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>
        /// If the provided model is not valid returns a BadRequest with the state of the model, 
        /// If the result is successful, returns the Ok status code with edeted model
        /// </returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "Moderator")]
        public async Task<ActionResult<CommentModel>> PutComment(int id, CommentModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _commentService.UpdateAsync(model.Transform());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_commentService.Get(model.Id) == null)
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

        /// <summary>
        /// Delets comment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// If category doesn't exist return  NotFound
        /// If the result is successful return NoContent
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            var model = await _commentService.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            await _commentService.DeleteAsync(model.Id);

            return NoContent();
        }
    }
}