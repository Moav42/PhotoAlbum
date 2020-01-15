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
        
        public void CreatePost(Post item)
        {
            DB.Posts.Add(item);
        }

        public void DeletePost(int id)
        {
            var item = DB.Posts.Find(id);
            if (item != null)
            {
                DB.Posts.Remove(item);
            }
        }

        public Post ReadPost(int id)
        {
            return DB.Posts.Find(id);
        }

        public IEnumerable<Post> ReadAllPosts()
        {
            return DB.Posts;
        }

        public void UpdatePost(Post item)
        {
            DB.Posts.Update(item);
        }

        public IEnumerable<Post> ReadAllPostsByCategory(int categoryId)
        {
            var postCategories = DB.PostCategories.Where(ct => ct.CategoryId == categoryId);
            var posts = new List<Post>();
            foreach (var item in postCategories)
            {
                posts.Add(ReadPost(item.PostId));
            }
            return posts;
        }
    }
}
