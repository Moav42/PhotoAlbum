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
    public class TagsController : ControllerBase
    {
        private ITagService<TagBLL> _tagService;
        public TagsController(ITagService<TagBLL> tagService)
        {
            _tagService = tagService;

        }

        [HttpGet]
        public ActionResult<IEnumerable<TagModel>> GetTags()
        {
            var tagsBLL = _tagService.GetAll();
            var tagModels = new List<TagModel>();

            foreach (var item in tagsBLL)
            {
                tagModels.Add(item.Transform());
            }
            return tagModels;
        }

        [HttpGet("{id}")]
        public ActionResult<TagModel> GetTag(int id)
        {
            var tagsBLL = _tagService.Get(id);

            if(tagsBLL == null)
            {
                return BadRequest("Model doesn`t exicte");
            }
            else
            {
                return tagsBLL.Transform();
            }
            
        }

        [HttpPost]
        public ActionResult<TagModel> PostTag(TagModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            _tagService.Add(model.Transform());

            return CreatedAtAction("GetTag", new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public ActionResult<TagModel> PutTag(TagModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            if (_tagService.Get(model.Id) == null)
            {
                return BadRequest("Model doesn`t exicte");
            }
            _tagService.Update(model.Transform());
            return model;
        }

        [HttpDelete("{id}")]
        public ActionResult<TagModel> DeleteTag(int id)
        {
            var model = _tagService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _tagService.Delete(model.Id);

            return model.Transform();
        }
    }
}