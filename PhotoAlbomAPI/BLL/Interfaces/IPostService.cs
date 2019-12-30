using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPostService<T>
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
        Task<string> GetPathByIdAsync(int id);
        Task<IEnumerable<PostRateBLL>> GetRatesAsync(int postId);
        Task<IEnumerable<TagBLL>> GetTagsAsync(int postId);
        Task<IEnumerable<CategoryBLL>> GetCategoriesAsync(int postId);
        Task<IEnumerable<CommentBLL>> GetCommentsAsync(int postId);

    }
}
