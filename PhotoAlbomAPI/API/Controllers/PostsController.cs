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
        private IPostService<PostBLL> _postService;
        public PostsController(IPostService<PostBLL> postService)
        {
            _postService = postService;

        }

        [HttpGet]
        public ActionResult<IEnumerable<PostModel>> GetTags()
        {
            var modelBLL = _postService.GetAll();
            var models = new List<PostModel>();

            foreach (var item in modelBLL)
            {
                models.Add(item.Transform());
            }
            return models;
        }

        [HttpGet("{id}")]
        public ActionResult<PostModel> GetTag(int id)
        {
            var modelBLL = _postService.Get(id);

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
        public ActionResult<PostModel> PostTag(PostModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            //model.AddingDate = DateTime.Now;
            _postService.Add(model.Transform());

            return CreatedAtAction("GetPost", new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public ActionResult<PostModel> PutTag(PostModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            if (_postService.Get(model.Id) == null)
            {
                return BadRequest("Model doesn`t exicte");
            }
            _postService.Update(model.Transform());
            return model;
        }

        [HttpDelete("{id}")]
        public ActionResult<PostModel> DeleteTag(int id)
        {
            var model = _postService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _postService.Delete(model.Id);

            return model.Transform();
        }
    }
}