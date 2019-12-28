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
    public class PostRatesController : ControllerBase
    {
        private readonly IPostRateService<PostRateBLL> _postRateService;
        public PostRatesController(IPostRateService<PostRateBLL> postRateService)
        {
            _postRateService = postRateService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostRateModel>>> GetRateByPost([FromBody] int postId)
        {
            var modelBLL = await _postRateService.GetAllByPostAsync(postId);
            var models = new List<PostRateModel>();

            foreach (var item in modelBLL)
            {
                models.Add(item.Transform());
            }
            return models;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PostRateModel>>> GetRateByuser([FromBody] int postId)
        //{
        //    var modelBLL = await _postRateService.GetAllByPostAsync(postId);
        //    var models = new List<PostRateModel>();

        //    foreach (var item in modelBLL)
        //    {
        //        models.Add(item.Transform());
        //    }
        //    return models;
        //}



        //[HttpPost]
        //public async Task<ActionResult<PostRateModel>> PostCategory(PostRateModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Not a valid model");
        //    }

        //    await _postRateService.AddAsync(model.Transform());

        //    return CreatedAtAction("GetCategory", new { id = model.Id }, model);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<PostRateModel>> PutCategory(PostRateModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Not a valid model");
        //    }
        //    if (_postRateService.GetAsync(model.Id) == null)
        //    {
        //        return BadRequest("Model doesn`t exicte");
        //    }
        //    await _postRateService.UpdateAsync(model.Transform());
        //    return model;
        //}
    }
}