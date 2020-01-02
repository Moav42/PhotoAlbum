using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IPostTagsRepository<T>
    {
        IEnumerable<T> ReadAll();
        void Create(T item);
        void Delete(T item);

    }
}
