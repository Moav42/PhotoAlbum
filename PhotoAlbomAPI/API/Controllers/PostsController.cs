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
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PostsController : ControllerBase
    {
        private readonly IPostService<PostBLL> _postService;
        private readonly IPostRateService<PostRateBLL> _postRateService;

        public PostsController(IPostService<PostBLL> postService, IPostRateService<PostRateBLL> postRateService)
        {
            _postService = postService;
            _postRateService = postRateService;
        }

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

        [HttpPost("{id}/rate")]
        public async Task<ActionResult<bool>> GetPostRateByUser(int id, PostRateViewModel postRate )
        {
            return await _postRateService.GetPostRate(postRate.PostId, postRate.UserId);
        }
    }
}