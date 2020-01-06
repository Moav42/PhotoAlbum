using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class PostRepository : IPostRepository<Post>
    {
        private readonly DbContext DB;

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

        public IEnumerable<Post> ReadAllPostsByCategory(int categoryId)
        {
            var postCategories = DB.PostCategories.Where(ct => ct.CategoryId == categoryId);
            var posts = new List<Post>();
            foreach (var item in postCategories)
            {
                posts.Add(Read(item.PostId));
            }
            return posts;
        }
    }
}
