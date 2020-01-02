using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICommentService<T>
    {
        Task<IEnumerable<T>> GetByUserAsync(string userId);
        Task<IEnumerable<T>> GetByPostAsync(int postId);
        Task<T> GetAsync(int id);
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
    }
}
