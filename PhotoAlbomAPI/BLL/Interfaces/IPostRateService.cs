using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface IPostRateService<T>
    {
        Task<IEnumerable<T>> GetAllByPostAsync(int postId);
        Task AddAsync(T item);
        Task UpdateAsync(T item);
    }
}
