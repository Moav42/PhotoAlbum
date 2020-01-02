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

        public void Create(Comment item)
        {
            DB.Comments.Add(item);
        }

        public void Delete(int id)
        {
            var item = DB.Comments.Find(id);
            if (item != null)
            {
                DB.Comments.Remove(item);
            }
        }

        public Comment Read(int id)
        {
            return DB.Comments.Find(id);
        }

        public IEnumerable<Comment> ReadByPost(int postId)
        {
            return DB.Comments.Where(c => c.PostId == postId);
        }

        public IEnumerable<Comment> ReadByUser(string userId)
        {
            return DB.Comments.Where(c => c.UserId == userId);
        }

        public IEnumerable<Comment> ReadAll()
        {
            return DB.Comments;
        }

        public void Update(Comment item)
        {
            DB.Comments.Update(item);
        }
    }
}
