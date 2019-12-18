using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;

namespace DAL.Repositories
{
    public class PostTagsRepository : IPostTagsRepository<PostTags>
    {
        private DbContext DB;
        public PostTagsRepository(DbContext context)
        {
            DB = context;
        }

        public void Create(PostTags item)
        {
            DB.PostTags.Add(item);
        }

        public void Delete(int id)
        {
            var item = DB.PostTags.Find(id);
            if (item != null)
            {
                DB.PostTags.Remove(item);
            }

        }

        public PostTags Read(int id)
        {
            return DB.PostTags.Find(id);
        }

        public IEnumerable<PostTags> ReadAll()
        {
            return DB.PostTags;
        }

        public void Update(PostTags item)
        {
            DB.PostTags.Update(item);
        }
    }
}
