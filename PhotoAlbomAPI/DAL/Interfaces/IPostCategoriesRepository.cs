using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IPostCategoriesRepository<T>
    {
        IEnumerable<T> ReadAll();
        void Create(T item);
        void Delete(T item);
    }
}
