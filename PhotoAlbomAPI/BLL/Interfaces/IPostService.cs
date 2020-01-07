using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPostService<T>
    {
        T Get(int id);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllByCategoryAsync(int categoryId);
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
        Task<string> GetPathByIdAsync(int id);
        Task<IEnumerable<TagBLL>> GetTagsAsync(int postId);
        Task<IEnumerable<CommentBLL>> GetCommentsAsync(int postId);

    }
}
