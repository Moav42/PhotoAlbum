using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class CommentRepository : ICommentRepository<Comment>
    {
        private readonly DbContext DB;

        public CommentRepository(DbContext context)
        {
            DB = context;
        }

        public void CreateComment(Comment item)
        {
            DB.Comments.Add(item);
        }

        public void DeleteComment(int id)
        {
            var item = DB.Comments.Find(id);
            if (item != null)
            {
                DB.Comments.Remove(item);
            }
        }

        public Comment ReadComment(int id)
        {
            return DB.Comments.Find(id);
        }

        public IEnumerable<Comment> ReadAllCommentsByPost(int postId)
        {
            return DB.Comments.Where(c => c.PostId == postId);
        }

        public IEnumerable<Comment> ReaAllCommentsdByUser(string userId)
        {
            return DB.Comments.Where(c => c.UserId == userId);
        }

        public void UpdateComment(Comment item)
        {
            DB.Comments.Update(item);
        }
    }
}
