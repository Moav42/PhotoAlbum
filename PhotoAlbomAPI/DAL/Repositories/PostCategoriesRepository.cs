using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;

namespace DAL.Repositories
{
    class PostCategoriesRepository : IPostCategoriesRepository<PostCategories>
    {
        private DbContext DB;
        public PostCategoriesRepository(DbContext context)
        {
            DB = context;
        }

        public void Create(PostCategories item)
        {
            DB.PostCategories.Add(item);
        }

        public void Delete(int id)
        {
            var item = DB.PostCategories.Find(id);
            if (item != null)
            {
                DB.PostCategories.Remove(item);
            }

        }

        public PostCategories Read(int id)
        {
            return DB.PostCategories.Find(id);
        }

        public IEnumerable<PostCategories> ReadAll()
        {
            return DB.PostCategories;
        }

        public void Update(PostCategories item)
        {
            DB.PostCategories.Update(item);
        }
    }
}
