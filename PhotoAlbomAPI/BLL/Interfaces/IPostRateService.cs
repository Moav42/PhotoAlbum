using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPostRateService<T>
    {
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task<bool> GetPostRate(int postId, string userId);
    }
}
