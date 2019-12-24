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
        private ICategoryService<CategoryBLL> _categoryService;
        public CategoriesController(ICategoryService<CategoryBLL> categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryModel>> GetCategories()
        {
            var modelBLL = _categoryService.GetAll();
            var models = new List<CategoryModel>();

            foreach (var item in modelBLL)
            {
                models.Add(item.Transform());
            }
            return models;
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryModel> GetCategory(int id)
        {
            var modelBLL = _categoryService.Get(id);

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
        public ActionResult<CategoryModel> PostCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            _categoryService.Add(model.Transform());

            return CreatedAtAction("GetCategory", new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public ActionResult<CategoryModel> PutCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }
            if (_categoryService.Get(model.Id) == null)
            {
                return BadRequest("Model doesn`t exicte");
            }
            _categoryService.Update(model.Transform());
            return model;
        }

        [HttpDelete("{id}")]
        public ActionResult<CategoryModel> DeleteCategory(int id)
        {
            var model = _categoryService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _categoryService.Delete(model.Id);

            return model.Transform();
        }
    }
}