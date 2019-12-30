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
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
    [Route("api/[controller]")]
  
    //[Authorize(Policy = "Admin&Organisation")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService<TagBLL> _tagService;
        public TagsController(ITagService<TagBLL> tagService)
        {
            _tagService = tagService;

        }

        
        //[Authorize(Policy = "Admin&Organisation")]
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

        //[Authorize(Policy = "Organisation")]
        [HttpPost]
        public async Task<ActionResult<TagModel>> PostTag(TagModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            await _tagService.AddAsync(model.Transform());

            return new OkObjectResult(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TagModel>> PutTag(int id, TagModel model)
        {

            if (ModelState.IsValid)
            {
                if (id != model.Id)
                {
                    return BadRequest();
                }
                try
                {
                    await _tagService.UpdateAsync(model.Transform());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _tagService.GetAsync(model.Id) == null)
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