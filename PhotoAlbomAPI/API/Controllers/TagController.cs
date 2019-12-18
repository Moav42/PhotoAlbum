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

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private TagService _tagService = new TagService();

        [HttpGet]
        public ActionResult<IEnumerable<TagModel>> GetTags()
        {
            var tagsBLL = _tagService.GetTags();
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
            var tagsBLL = _tagService.GetTag(id);
            return tagsBLL.Transform();
        }

        [HttpPost]
        public ActionResult<TagModel> PostTag(TagModel model)
        {
            var tag = model.Transform();
            _tagService.AddTag(tag);

            return CreatedAtAction("GetTag", new { id = model.Id }, model);
        }
    }
}