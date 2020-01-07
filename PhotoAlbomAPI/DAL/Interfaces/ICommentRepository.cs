using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ICommentRepository<T>
    {
        T ReadComment(int id);
        void CreateComment(T item);
        void UpdateComment(T item);
        void DeleteComment(int id);
        IEnumerable<T> ReadAllCommentsByPost(int postId);
        IEnumerable<T> ReaAllCommentsdByUser(string userId);
    
    }
}
