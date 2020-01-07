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
    public class CommentRepository : ICommentRepository<Comment>
    {
        private readonly DbContext DB;

        /// <summary>
        /// Configure repository by DbContext, provides by UoF
        /// </summary>
        /// <param name="context"></param>
        public CommentRepository(DbContext context)
        {
            DB = context;
        }

        /// <summary>
        /// Creates new Comment
        /// </summary>
        /// <param name="item"></param>
        public void Create(Comment item)
        {
            DB.Comments.Add(item);
        }

        /// <summary>
        /// Delets Comment
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var item = DB.Comments.Find(id);
            if (item != null)
            {
                DB.Comments.Remove(item);
            }
        }

        /// <summary>
        /// Gets Comment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Comment Read(int id)
        {
            return DB.Comments.Find(id);
        }

        /// <summary>
        /// Gets all comments of post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public IEnumerable<Comment> ReadByPost(int postId)
        {
            return DB.Comments.Where(c => c.PostId == postId);
        }

        /// <summary>
        /// Gets all comments of user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Comment> ReadByUser(string userId)
        {
            return DB.Comments.Where(c => c.UserId == userId);
        }

        /// <summary>
        /// Updates Comment
        /// </summary>
        /// <param name="item"></param>
        public void Update(Comment item)
        {
            DB.Comments.Update(item);
        }
    }
}
