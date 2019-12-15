using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;

namespace DAL.Repositories
{
    class CommentRepository : ICommentRepository<Comment>
    {
        private DbContext DB;
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
