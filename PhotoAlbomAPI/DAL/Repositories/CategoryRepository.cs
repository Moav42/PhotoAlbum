using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository<Category>
    {
        private DbContext DB;
        public CategoryRepository(DbContext context)
        {
            DB = context;
        }

        public void Create(Category item)
        {
            DB.Categories.Add(item);
        }

        public void Delete(int id)
        {
            Category category = DB.Categories.Find(id);
            if (category != null)
            {
                DB.Categories.Remove(category);
            }

        }

        public Category Read(int id)
        {
            return DB.Categories.Find(id);
        }

        public IEnumerable<Category> ReadAll()
        {
            return DB.Categories;
        }

        public void Update(Category item)
        {
            DB.Categories.Update(item);
        }
    }
}
