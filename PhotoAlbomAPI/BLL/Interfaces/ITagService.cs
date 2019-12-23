using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ITagService<T>
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}
