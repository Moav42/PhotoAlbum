using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPostService<T>
    {
        Task<T> GetPostAsync(int id);
        Task<IEnumerable<T>> GetAllPostsAsync();
        Task<IEnumerable<T>> GetAllPostsByCategoryAsync(int categoryId);
        Task AddPostAsync(T item);
        Task UpdatePostAsync(T item);
        Task DeletePostAsync(int id);
        Task<string> GetImagePathByIdAsync(int id);
        Task<IEnumerable<TagBLL>> GetAllPostTagsAsync(int postId);
        Task<IEnumerable<CommentBLL>> GetAllPostCommentsAsync(int postId);

    }
}
