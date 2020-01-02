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

        public void Create(PostRate item)
        {
            DB.PostRates.Add(item);
        }

        public void Update(PostRate item)
        {
            DB.PostRates.Update(item);
            DB.SaveChanges();
        }

        public IEnumerable<PostRate> ReadAllByPost(int postID)
        {
            return DB.PostRates.Where(pr => pr.PostId == postID);
        }
        public IEnumerable<PostRate> ReadAllByUser(string userId)
        {
            return DB.PostRates.Where(pr => pr.UserId == userId);
        }

    }
}
