using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;

namespace DAL.Repositories
{
    public class PostRepository : IPostRepository<Post>
    {
        private DbContext DB;
        public PostRepository(DbContext context)
        {
            DB = context;
        }

        public void Create(Post item)
        {
            DB.Posts.Add(item);
        }

        public void Delete(int id)
        {
            var item = DB.Posts.Find(id);
            if (item != null)
            {
                DB.Posts.Remove(item);
            }

        }

        public Post Read(int id)
        {
            return DB.Posts.Find(id);
        }

        public IEnumerable<Post> ReadAll()
        {
            return DB.Posts;
        }

        public void Update(Post item)
        {
            DB.Posts.Update(item);
        }
    }
}
