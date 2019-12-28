using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPostRateService<T>
    {
        Task<IEnumerable<T>> GetAllByPostAsync(int postId);
        Task<IEnumerable<T>> GetAllByUserAsync(string usertId);
        Task AddAsync(T item);
        Task UpdateAsync(T item);
    }
}
