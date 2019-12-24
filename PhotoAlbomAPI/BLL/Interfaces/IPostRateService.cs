using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    interface IPostRateService<T>
    {
        IEnumerable<T> GetAllByPost(int postId);
        void Add(T item);
        void Update(T item);
    }
}
