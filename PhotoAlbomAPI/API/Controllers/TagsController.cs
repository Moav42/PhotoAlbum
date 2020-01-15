using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using BLL.Models;
using API.Extensions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using API.Models.ViewModels;

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

        [Authorize(Policy = "AllUsers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagModel>>> GetTags()
        {
            var tagsBLL = await _tagService.GetAllTagsAsync();
            var tagModels = new List<TagModel>();

            foreach (var item in tagsBLL)
            {
                tagModels.Add(item.Transform());
            }
            if (tagModels.Count == 0)
            {
                return NotFound();
            }
            return Ok(tagModels);
        }

        [Authorize(Policy = "AllUsers")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TagModel>> GetTag(int id)
        {
            var tagsBLL = await _tagService.GetTagAsync(id);

            if(tagsBLL == null)
            {
                return NotFound("Model doesn`t exicte");
            }
            else
            {
                return Ok(tagsBLL.Transform());
            }           
        }

        [Authorize(Policy = "Organisation")]
        [HttpPost]
        public async Task<ActionResult<TagModel>> PostTag(TagModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _tagService.AddTagAsync(model.Transform());

            return CreatedAtAction(nameof(GetTag), new { id = model.Id }, model);
        }

        [Authorize(Policy = "Organisation")]
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
                    await _tagService.UpdateTagAsync(model.Transform());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _tagService.GetTagAsync(model.Id) == null)
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

        [Authorize(Policy = "Organisation")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTag(int id)
        {
            var model = await _tagService.GetTagAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            await _tagService.DeleteTagAsync(model.Id);

            return NoContent();
        }


        [Authorize(Policy = "Moderator")]
        [HttpPost("post")]
        public async Task<ActionResult<PostTagViewModel>> AddTagToPost(PostTagViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            await _tagService.AddTagToPostAsync(int.Parse(model.PostId),int.Parse(model.TagId));
            return Ok(model);
        }
    }
}