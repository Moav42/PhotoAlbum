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

        public void AddPostToCategory(PostCategories item)
        {
            DB.PostCategories.Add(item);
        }

        public void DeletePostFromCategory(PostCategories item)
        {
            DB.PostCategories.Remove(item);
        }
  

    }
}
