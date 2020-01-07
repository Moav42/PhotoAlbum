using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ICategoryRepository<T>
    {
        IEnumerable<T> ReadAllCategories();
        T ReadCategory(int id);
        void CreateCategory(T item);
        void UpdateCategory(T item);
        void DeleteCategory(int id);
    }
}
