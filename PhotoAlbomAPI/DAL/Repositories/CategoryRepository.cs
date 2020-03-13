using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Collections.Generic;

namespace DAL.Repositories
{

    public class CategoryRepository : ICategoryRepository<Category>
    {
        private readonly DbContext DB;

        public CategoryRepository(DbContext context)
        {
            DB = context;
        }


        public void CreateCategory(Category item)
        {
            DB.Categories.Add(item);
        }

        public void DeleteCategory(int id)
        {
            Category category = DB.Categories.Find(id);
            if (category != null)
            {
                DB.Categories.Remove(category);
            }
        }

        public Category ReadCategory(int id)
        {
            return DB.Categories.Find(id);
        }

        public IEnumerable<Category> ReadAllCategories()
        {
            return DB.Categories;
        }

        public void UpdateCategory(Category item)
        {
            DB.Categories.Update(item);
        }     
    }
}
