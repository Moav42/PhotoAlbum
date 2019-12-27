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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService<CategoryBLL> _categoryService;
        public CategoriesController(ICategoryService<CategoryBLL> categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategories()
        {
            var modelBLL = await _categoryService.GetAllAsync();
            var models = new List<CategoryModel>();

            foreach (var item in modelBLL)
            {
                models.Add(item.Transform());
            }
            return models;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetCategory(int id)
        {
            var modelBLL = await _categoryService.GetAsync(id);

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
        public async Task<ActionResult<CategoryModel>> PostCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            await _categoryService.AddAsync(model.Transform());

            return CreatedAtAction("GetCategory", new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryModel>> PutCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            if (_categoryService.GetAsync(model.Id) == null)
            {
                return BadRequest("Model doesn`t exicte");
            }
            await _categoryService.UpdateAsync(model.Transform());
            return model;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryModel>> DeleteCategory(int id)
        {
            var model = await _categoryService.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            await  _categoryService.DeleteAsync(model.Id);

            return model.Transform();
        }
    }
}