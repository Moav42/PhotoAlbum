using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IPostRateRepository<T>
    {
        IEnumerable<T> ReadAllByPost(int postID);
    }
}
