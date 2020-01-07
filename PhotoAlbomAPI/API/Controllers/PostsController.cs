using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using BLL.Models;
using API.Extensions;
using BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using API.Models.ViewModels;

namespace API.Controllers
{
    /// <summary>
    /// A controller representing functionality to manage the corresponding resource
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PostsController : ControllerBase
    {
        private readonly IPostService<PostBLL> _postService;
        private readonly IPostRateService<PostRateBLL> _postRateService;

        /// <summary>
        /// Configures the controller with the appropriate services using the dependency injection 
        /// </summary>
        /// <param name="organisationService"></param>
        /// <param name="accountService"></param>
        public PostsController(IPostService<PostBLL> postService, IPostRateService<PostRateBLL> postRateService)
        {
            _postService = postService;
            _postRateService = postRateService;
        }

        /// <summary>
        /// Gets all posts
        /// </summary>
        /// <returns>If result success returns posts, if it's not return NotFound</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetPosts()
        {
            var modelBLL = await _postService.GetAllPostsAsync();
            var models = new List<PostModel>();

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
        /// Gets post by id
        /// </summary>
        /// <returns>If result success returns post, if it's not return NotFound</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PostModel>> GetPost(int id)
        {
            var modelBLL = await _postService.GetPostAsync(id);

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
        /// Gets image file by given post id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet("{postId}/image")]
        public async Task<ActionResult> GetPostImageById(int postId)
        {
            string path = await _postService.GetImagePathByIdAsync(postId);
            if(path != null)
            {
                Byte[] b = await System.IO.File.ReadAllBytesAsync(path);
                return File(b, "image/jpeg");
            }
            else
            {
                return NotFound("Post not found ");
            }      
        }

        /// <summary>
        /// Creates new Post
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// If the provided model is not valid returns a BadRequest with the state of the model, 
        /// If the result is successful, returns the created Created status code with the model
        /// </returns>
        [HttpPost]
        [Authorize(Policy = "AllUsers")]
        public async Task<ActionResult<PostModel>> PostPost(PostModel model)
        {           
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }           
            await _postService.AddPostAsync(model.Transform());

            return CreatedAtAction(nameof(GetPost), new { id = model.Id }, model);
        }

        /// <summary>
        /// Uploads image to the server
        /// </summary>
        /// <returns>
        /// If file wasn't attached return, or other errors occurred return status code 400 or 500
        /// If result sucses return status code 200
        /// </returns>
        [HttpPost("uplaodImage")]
        [Authorize(Policy = "AllUsers")]
        public IActionResult UplaodImage()
        {
            try
            {
                var file = Request.Form.Files[0];             
                var folderName = Path.Combine("images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error. Did you forget to attach the file?");
            }
        }

        /// <summary>
        /// Edits post
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>
        /// If the provided model is not valid returns a BadRequest with the state of the model, 
        /// If the result is successful, returns the Ok status code with edeted model
        /// </returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "Moderator")]
        public async Task<ActionResult> PutPost(int id, PostModel model)
        {         
            if (ModelState.IsValid)
            {
                if (id != model.Id)
                {
                    return BadRequest();
                }
                try
                {
                    await _postService.UpdatePostAsync(model.Transform());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _postService.GetPostAsync(model.Id) == null)
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
        /// Delets Post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// If category doesn't exist return  NotFound
        /// If the result is successful return NoContent
        /// </returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Moderator")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var model = await _postService.GetPostAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            await _postService.DeletePostAsync(model.Id);

            return NoContent();
        }

        /// <summary>
        /// Adds a post rating from the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>
        /// If the provided model is not valid returns a BadRequest with the state of the model, 
        /// If the result is successful, returns the created Created status code with the model
        /// </returns>
        [HttpPost("{id}/like")]
        [Authorize(Policy = "AllUsers")]
        public async Task<ActionResult<PostRateModel>> PostRate(int id, PostRateModel model)
        {
            if(id != model.PostId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _postRateService.AddRateToPostAsync(model.Transform());

            return Ok(model);
        }

        /// <summary>
        /// Gets all comments of given post
        /// </summary>
        /// <returns>If result success returns posts, if it's not return NotFound</returns>
        [HttpGet("{postId}/comments")]
        public async Task<ActionResult<IEnumerable<CommentModel>>> GetPostComments(int postId)
        {
            var modelBLL = await _postService.GetAllPostCommentsAsync(postId);
            var models = new List<CommentModel>();

            foreach (var item in modelBLL)
            {
                models.Add(item.Transform());
            }

            if (models.Count == 0)
            {
                return NotFound();
            }

            return models;
        }

        /// <summary>
        /// Gets all tags of given post
        /// </summary>
        /// <returns>If result success returns posts, if it's not return NotFound</returns>
        [HttpGet("{postId}/tags")]
        public async Task<ActionResult<IEnumerable<TagModel>>> GetPostTags(int postId)
        {
            var modelBLL = await _postService.GetAllPostTagsAsync(postId);
            var models = new List<TagModel>();

            foreach (var item in modelBLL)
            {
                models.Add(item.Transform());
            }

            if (models.Count == 0)
            {
                return NotFound();
            }

            return models;
        }

        /// <summary>
        /// Determines if a user rated a post
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postRate"></param>
        /// <returns>If the user rated the post returns true, else folse</returns>
        [HttpPost("{id}/rate")]
        public async Task<ActionResult<bool>> GetPostRateByUser(int id, PostRateViewModel postRate )
        {
            return await _postRateService.GetPostRate(postRate.PostId, postRate.UserId);
        }
    }
}