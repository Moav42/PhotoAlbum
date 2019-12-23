using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ICommentService<T>
    {
        T Get(int id);
        IEnumerable<T> GetByUser(string userId);
        IEnumerable<T> GetByPost(int postId);
        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}
