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
    public class PostCategoriesRepository : IPostCategoriesRepository<PostCategories>
    {
        private readonly DbContext DB;

        /// <summary>
        /// Configure repository by DbContext, provides by UoF
        /// </summary>
        /// <param name="context"></param>
        public PostCategoriesRepository(DbContext context)
        {
            DB = context;
        }

        /// <summary>
        /// Adds category to post
        /// </summary>
        /// <param name="item"></param>
        public void Create(PostCategories item)
        {
            DB.PostCategories.Add(item);
        }

        /// <summary>
        /// Deletes category from post
        /// </summary>
        /// <param name="item"></param>
        public void Delete(PostCategories item)
        {
            DB.PostCategories.Remove(item);
        }
  

    }
}
