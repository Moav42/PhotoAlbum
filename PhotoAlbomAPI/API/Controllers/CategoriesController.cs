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
    /// <summary>
    /// A controller representing functionality to manage the corresponding resource
    /// </summary>
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

        /// <summary>
        /// Gets all Categories
        /// </summary>
        /// <returns>If result success returns Categories, if it's not return NotFound</returns>
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
            if(models.Count == 0)
            {
                return NotFound();
            }
            return Ok(models);
        }

        /// <summary>
        /// Gets category by id
        /// </summary>
        /// <returns>If result success returns Category, if it's not return NotFound</returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "AllUsers")]
        public async Task<ActionResult<CategoryModel>> GetCategory(int id)
        {
            var modelBLL = await _categoryService.GetAsync(id);

            if (modelBLL == null)
            {
                return NotFound("Model doesn`t exicte");
            }
            else
            {
                return Ok(modelBLL.Transform());
            }
        }

        /// <summary>
        /// Creates new Category
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// If the provided model is not valid returns a BadRequest with the state of the model, 
        /// If the result is successful, returns the created Created status code with the model
        /// </returns>
        [HttpPost]
        [Authorize(Policy = "Organisation")]
        public async Task<ActionResult<CategoryModel>> PostCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryService.AddAsync(model.Transform());

            return CreatedAtAction(nameof(GetCategory), new { id = model.Id }, model);
        }

        /// <summary>
        /// Edits Category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>
        /// If the provided model is not valid returns a BadRequest with the state of the model, 
        /// If the result is successful, returns the Ok status code with edeted model
        /// </returns>
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
                    if (_categoryService.Get(id) == null)
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

        /// <summary>
        /// Delets Category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// If category doesn't exist return  NotFound
        /// If the result is successful return NoContent
        /// </returns>
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

        /// <summary>
        /// Add post to the category 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// If the provided model is not valid returns a BadRequest with the state of the model, 
        /// If the result is successful, returns the Ok status code with edeted model
        /// </returns>
        [HttpPost("post")]
        [Authorize(Policy = "Moderator")]
        public async Task<ActionResult<PostCategoryViewModel>> AddPostToCategory(PostCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryService.AddCategoryToPostAsync(int.Parse(model.PostId), int.Parse(model.CategoryId));

            return model;
        }

        /// <summary>
        /// Gets all posts of given category
        /// </summary>
        /// <returns>If result success returns Posts, if it's not return NotFound</returns>
        [HttpGet("posts/{categoryId}")]
        [Authorize(Policy = "AllUsers")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetPostsByCategory(int categoryId)
        {
            var modelBLL = await _postService.GetAllByCategoryAsync(categoryId);
            var models = new List<PostModel>();

            foreach (var item in modelBLL)
            {
                models.Add(item.Transform());
            }

            if (models.Count == 0)
            {
                return NotFound();
            }

            return models;
        }

    }
}