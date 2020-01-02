using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class PostCategoriesRepository : IPostCategoriesRepository<PostCategories>
    {
        private readonly DbContext DB;

        public PostCategoriesRepository(DbContext context)
        {
            DB = context;
        }

        public void Create(PostCategories item)
        {
            DB.PostCategories.Add(item);
        }

        public void Delete(PostCategories item)
        {
            DB.PostCategories.Remove(item);
        }

        public IEnumerable<PostCategories> ReadAll()
        {
            return DB.PostCategories;
        }

    }
}
