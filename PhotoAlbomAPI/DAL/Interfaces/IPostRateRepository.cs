using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IPostRateRepository<T>
    {
        void AddRateToPost(T item);
        void UpdatePostRate(T item);
        bool GetPostsRate(int postId, string userId);
    }
}
