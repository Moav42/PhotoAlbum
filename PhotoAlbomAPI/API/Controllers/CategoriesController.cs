using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using BLL.Models;
using API.Extensions;
using BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using API.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService<CategoryBLL> _categoryService;
        private readonly IPostService<PostBLL> _postService;

        public CategoriesController(ICategoryService<CategoryBLL> categoryService, IPostService<PostBLL> postService)
        {
            _categoryService = categoryService;
            _postService = postService;
        }

        [HttpGet]
        [Authorize(Policy = "AllUsers")]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategories()
        {
            var modelBLL = await _categoryService.GetAllAsync();
            var models = new List<CategoryModel>();

            foreach (var item in modelBLL)
            {
                models.Add(item.Transform());
            }
            return Ok(models);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AllUsers")]
        public async Task<ActionResult<CategoryModel>> GetCategory(int id)
        {
            var modelBLL = await _categoryService.GetAsync(id);

            if (modelBLL == null)
            {
                return BadRequest("Model doesn`t exicte");
            }
            else
            {
                return Ok(modelBLL.Transform());
            }
        }

        [HttpPost]
        [Authorize(Policy = "Organisation")]
        public async Task<ActionResult<CategoryModel>> PostCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            await _categoryService.AddAsync(model.Transform());

            return CreatedAtAction(nameof(GetCategory), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Organisation")]
        public async Task<ActionResult<CategoryModel>> PutCategory(int id, CategoryModel model)
        {

            if (ModelState.IsValid)
            {
                if (id != model.Id)
                {
                    return BadRequest("Model id wrong");
                }

                try
                {
                    await _categoryService.UpdateAsync(model.Transform());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _categoryService.GetAsync(id) == null)
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
        [Authorize(Policy = "Organisation")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var model = await _categoryService.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteAsync(model.Id);

            return NoContent();
        }

        [HttpPost("post")]
        [Authorize(Policy = "AllUsers")]
        public async Task<ActionResult<PostCategoryViewModel>> AddPostToCategory(PostCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            await _categoryService.AddCategoryToPostAsync(int.Parse(model.PostId), int.Parse(model.CategoryId));

            return model;
        }

        [HttpGet("posts/{categoryId}")]
        [Authorize(Policy = "AllUsers")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetPosts(int categoryId)
        {
            var modelBLL = await _postService.GetAllByCategoryAsync(categoryId);
            var models = new List<PostModel>();

            foreach (var item in modelBLL)
            {
                models.Add(item.Transform());
            }

            return models;
        }

    }
}