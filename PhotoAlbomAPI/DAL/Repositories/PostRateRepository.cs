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
    public class PostRateRepository : IPostRateRepository<PostRate>
    {
        private readonly DbContext DB;

        /// <summary>
        /// Configure repository by DbContext, provides by UoF
        /// </summary>
        /// <param name="context"></param>
        public PostRateRepository(DbContext context)
        {
            DB = context;
        }

        /// <summary>
        /// Adds rate to post
        /// </summary>
        /// <param name="item"></param>
        public void Create(PostRate item)
        {
            DB.PostRates.Add(item);
        }

        /// <summary>
        /// Updates rate of post
        /// </summary>
        /// <param name="item"></param>
        public void Update(PostRate item)
        {
            DB.PostRates.Update(item);
            DB.SaveChanges();
        }

        /// <summary>
        /// Determines if a user rated a post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool GetPostsRate(int postId, string userId)
        {
            var rate = DB.PostRates.Where(pr => pr.PostId == postId && pr.UserId == userId).ToList();
            if(rate.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
