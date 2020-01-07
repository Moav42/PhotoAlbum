using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IPostCategoriesRepository<T>
    {
        void AddPostToCategory(T item);
        void DeletePostFromCategory(T item);
    }
}
