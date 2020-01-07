using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryService<T>
    {
        Task<T> GetCategoryAsync(int id);
        Task<IEnumerable<T>> GetAllCategoriesAsync();
        Task AddCategoryAsync(T item);
        Task UpdateCategoryAsync(T item);
        Task DeleteCategoryAsync(int id);
        Task AddCategoryToPostAsync(int postId, int categoryId);

    }
}
