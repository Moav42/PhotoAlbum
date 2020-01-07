using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITagService<T>
    {
        Task<T> GetTagAsync(int id);     
        Task<IEnumerable<T>> GetAllTagsAsync();
        Task AddTagAsync(T item);
        Task UpdateTagAsync(T item);
        Task DeleteTagAsync(int id);
        Task AddTagToPostAsync(int postId, int tagId);
    }
}
