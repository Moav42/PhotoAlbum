using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IPostRepository<T>
    {
        IEnumerable<T> ReadAll();
        T Read(int id);
        void Create(T item);
        void Update(int id, T item);
        void Delete(int id);
    }
}
