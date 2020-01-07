using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IPostRepository<T>
    {
        IEnumerable<T> ReadAllPosts();
        T ReadPost(int id);
        void CreatePost(T item);
        void UpdatePost(T item);
        void DeletePost(int id);
        IEnumerable<T> ReadAllPostsByCategory(int categoryId);
    }
}
