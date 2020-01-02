using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IPostRepository<T>
    {
        IEnumerable<T> ReadAll();
        T Read(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        IEnumerable<T> ReadAllPostsByCategory(int categoryId);
    }
}
