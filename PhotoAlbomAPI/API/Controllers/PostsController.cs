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
            var modelBLL = await _postService.GetAllAsync();
            var models = new List<PostModel>();

            foreach (var item in modelBLL)
            {
                models.Add(item.Transform());
            }
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostModel>> GetPost(int id)
        {
            var modelBLL = await _postService.GetAsync(id);

            if (modelBLL == null)
            {
                return BadRequest("Model doesn`t exicte");
            }
            else
            {
                return Ok(modelBLL.Transform());
            }
        }

        [HttpGet("{postId}/image")]
        public async Task<ActionResult> GetPostImageById(int postId)
        {
            string path = await _postService.GetPathByIdAsync(postId);
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
            await _postService.AddAsync(model.Transform());

            return CreatedAtAction(nameof(GetPost), new { id = model.Id }, model);
        }

        [HttpPost("uplaodImage")]
        [Authorize(Policy = "AllUsers")]
        public IActionResult UplaodImage()
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
                    await _postService.UpdateAsync(model.Transform());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _postService.GetAsync(model.Id) == null)
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
            return BadRequest("Not a valid model");
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Moderator")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var model = await _postService.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            await _postService.DeleteAsync(model.Id);

            return NoContent();
        }

        [HttpPost("rate")]
        [Authorize(Policy = "AllUsers")]
        public async Task<ActionResult<PostRateModel>> PostRate(PostRateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            await _postRateService.AddAsync(model.Transform());

            return Ok(model);
        }

        [HttpGet("{postId}/comments")]
        public async Task<ActionResult<IEnumerable<CommentModel>>> GetPostComments(int postId)
        {
            var modelBLL = await _postService.GetCommentsAsync(postId);
            var models = new List<CommentModel>();

            foreach (var item in modelBLL)
            {
                models.Add(item.Transform());
            }
            return models;
        }

        [HttpGet("{postId}/tags")]
        public async Task<ActionResult<IEnumerable<TagModel>>> GetPostTags(int postId)
        {
            var modelBLL = await _postService.GetTagsAsync(postId);
            var models = new List<TagModel>();

            foreach (var item in modelBLL)
            {
                models.Add(item.Transform());
            }
            return models;
        }
    }
}