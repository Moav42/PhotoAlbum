using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IPostTagsRepository<T>
    {
        void Create(T item);
        void Delete(T item);

    }
}
