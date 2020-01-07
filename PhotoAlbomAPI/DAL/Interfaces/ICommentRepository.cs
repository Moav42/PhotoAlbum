using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ICommentRepository<T>
    {
        T Read(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        IEnumerable<T> ReadByPost(int postId);
        IEnumerable<T> ReadByUser(string userId);
    
    }
}
