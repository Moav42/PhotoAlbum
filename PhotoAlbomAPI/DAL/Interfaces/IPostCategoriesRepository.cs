using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IPostCategoriesRepository<T>
    {
        void Create(T item);
        void Delete(T item);
    }
}
