using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPostRateService<T>
    {
        Task AddRateToPostAsync(T item);
        Task UpdatePostRateAsync(T item);
        Task<bool> GetPostRate(int postId, string userId);
    }
}
