using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IPostRateRepository<T>
    {
        void Create(T item);
        void Update(T item);
        IEnumerable<T> ReadAllByPost(int postID);
        IEnumerable<T> ReadAllByUser(string userId);
        bool GetPostsRate(int postId, string userId);
    }
}
