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
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return models;
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
                return modelBLL.Transform();
            }
        }

        [HttpGet("image/{postId}")]
        public async Task<ActionResult> GetPostImageById(int postId)
        {
            string path = await _postService.GetPathByIdAsync(postId);
            Byte[] b = await System.IO.File.ReadAllBytesAsync(path);
            return File(b, "image/jpeg");
        }

        [HttpPost]
        public async Task<ActionResult<PostModel>> PostPost(PostModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            await _postService.AddAsync(model.Transform());

            return new OkObjectResult(model);
        }

        [HttpPut("{id}")]
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
                return new OkObjectResult(model);
            }
            return BadRequest("Not a valid model");

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PostModel>> DeletePost(int id)
        {
            var model = await _postService.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            await _postService.DeleteAsync(model.Id);

            return model.Transform();
        }

        [HttpPost("rate")]
        public async Task<ActionResult<PostRateModel>> PostRate(PostRateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            await _postRateService.AddAsync(model.Transform());

            return new OkObjectResult(model);
        }
    }
}