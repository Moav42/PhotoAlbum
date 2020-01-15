using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class PostRateRepository : IPostRateRepository<PostRate>
    {
        private readonly DbContext DB;

        public PostRateRepository(DbContext context)
        {
            DB = context;
        }

        public void AddRateToPost(PostRate item)
        {
            DB.PostRates.Add(item);
        }

        public void UpdatePostRate(PostRate item)
        {
            DB.PostRates.Update(item);
            DB.SaveChanges();
        }

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
