using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    /// <summary>
    /// Represents an implementation of a repository pattern for the corresponding entity
    /// </summary>
    public class CategoryRepository : ICategoryRepository<Category>
    {
        private readonly DbContext DB;

        /// <summary>
        /// Configure repository by DbContext, provides by UoF
        /// </summary>
        /// <param name="context"></param>
        public CategoryRepository(DbContext context)
        {
            DB = context;
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="item"></param>
        public void CreateCategory(Category item)
        {
            DB.Categories.Add(item);
        }

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCategory(int id)
        {
            Category category = DB.Categories.Find(id);
            if (category != null)
            {
                DB.Categories.Remove(category);
            }
        }

        /// <summary>
        /// Gets category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category ReadCategory(int id)
        {
            return DB.Categories.Find(id);
        }

        /// <summary>
        /// Gets all categies
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> ReadAllCategories()
        {
            return DB.Categories;
        }

        /// <summary>
        /// Udates category
        /// </summary>
        /// <param name="item"></param>
        public void UpdateCategory(Category item)
        {
            DB.Categories.Update(item);
        }     
    }
}
