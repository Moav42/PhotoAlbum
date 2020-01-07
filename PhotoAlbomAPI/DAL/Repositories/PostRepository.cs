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
    public class PostRepository : IPostRepository<Post>
    {
        private readonly DbContext DB;

        /// <summary>
        /// Configure repository by DbContext, provides by UoF
        /// </summary>
        /// <param name="context"></param>
        public PostRepository(DbContext context)
        {
            DB = context;
        }
        
        /// <summary>
        /// Creates new post
        /// </summary>
        /// <param name="item"></param>
        public void CreatePost(Post item)
        {
            DB.Posts.Add(item);
        }

        /// <summary>
        /// Deletes post by id
        /// </summary>
        /// <param name="id"></param>
        public void DeletePost(int id)
        {
            var item = DB.Posts.Find(id);
            if (item != null)
            {
                DB.Posts.Remove(item);
            }
        }

        /// <summary>
        /// Gets post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Post ReadPost(int id)
        {
            return DB.Posts.Find(id);
        }

        /// <summary>
        /// Gets all posts
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Post> ReadAllPosts()
        {
            return DB.Posts;
        }

        /// <summary>
        /// Update post
        /// </summary>
        /// <param name="item"></param>
        public void UpdatePost(Post item)
        {
            DB.Posts.Update(item);
        }

        /// <summary>
        /// Gets all post of given category 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
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
