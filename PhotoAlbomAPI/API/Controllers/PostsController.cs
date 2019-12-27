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
    public class PostsController : ControllerBase
    {
        private readonly IPostService<PostBLL> _postService;
        public PostsController(IPostService<PostBLL> postService)
        {
            _postService = postService;
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

        [HttpPost]
        public async Task<ActionResult<PostModel>> PostPost(PostModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            await _postService.AddAsync(model.Transform());

            return CreatedAtAction("GetPost", new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PostModel>> PutPost(PostModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            if (_postService.GetAsync(model.Id) == null)
            {
                return BadRequest("Model doesn`t exicte");
            }
            await _postService.UpdateAsync(model.Transform());
            return model;
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
    }
}