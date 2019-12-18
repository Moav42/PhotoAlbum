using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;

namespace DAL.Repositories
{
    public class PostRateRepository : IPostRateRepository<PostRate>
    {
        private DbContext DB;
        public PostRateRepository(DbContext context)
        {
            DB = context;
        }

        public void Create(PostRate item)
        {
            DB.PostRates.Add(item);
        }

        public void Delete(int id)
        {
            var item = DB.PostRates.Find(id);
            if (item != null)
            {
                DB.PostRates.Remove(item);
            }

        }

        public PostRate Read(int id)
        {
            return DB.PostRates.Find(id);
        }

        public IEnumerable<PostRate> ReadAll()
        {
            return DB.PostRates;
        }

        public void Update(PostRate item)
        {
            DB.PostRates.Update(item);
        }
    }
}
