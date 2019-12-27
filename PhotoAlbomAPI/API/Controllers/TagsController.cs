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
        private readonly ITagService<TagBLL> _tagService;
        public TagsController(ITagService<TagBLL> tagService)
        {
            _tagService = tagService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagModel>>> GetTags()
        {
            var tagsBLL = await _tagService.GetAllAsync();
            var tagModels = new List<TagModel>();

            foreach (var item in tagsBLL)
            {
                tagModels.Add(item.Transform());
            }
            return tagModels;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TagModel>> GetTag(int id)
        {
            var tagsBLL = await _tagService.GetAsync(id);

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
        public async Task<ActionResult<TagModel>> PostTag(TagModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            await _tagService.AddAsync(model.Transform());

            return CreatedAtAction("GetTag", new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TagModel>> PutTag(TagModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            if (await _tagService.GetAsync(model.Id) == null)
            {
                return BadRequest("Model doesn`t exicte");
            }
            await _tagService.UpdateAsync(model.Transform());
            return model;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TagModel>> DeleteTag(int id)
        {
            var model = await _tagService.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            await _tagService.DeleteAsync(model.Id);

            return model.Transform();
        }
    }
}