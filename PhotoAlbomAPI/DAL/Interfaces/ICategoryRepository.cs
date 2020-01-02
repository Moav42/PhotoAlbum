using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ICategoryRepository<T>
    {
        IEnumerable<T> ReadAll();
        T Read(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        public IEnumerable<T> ReadAllByPost(int postId);

    }
}
