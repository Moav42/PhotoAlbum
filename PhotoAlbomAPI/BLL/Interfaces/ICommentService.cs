using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICommentService<T>
    {
        Task<IEnumerable<T>> GetAllCommentsByUserAsync(string userId);
        Task<IEnumerable<T>> GetByPostAsync(int postId);
        Task<T> GetCommentsAsync(int id);
        Task AddCommentAsync(T item);
        Task UpdateCommentAsync(T item);
        Task DeleteCommentAsync(int id);
    }
}
