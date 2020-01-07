using System.Collections.Generic;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Interfaces;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL.Services
{
    /// <summary>
    /// A service containing business logic that is responsible for a specific resource. Configurable by the UoF, implemented through the DI
    /// </summary>
    public class CategoryService : ICategoryService<CategoryBLL>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Configures the service with the parameters provided by the dependency injection system
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all categories asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryBLL>> GetAllCategoriesAsync()
        {
            var itemsDAL = await Task.Run(() => _unitOfWork.CategorysRepository.ReadAllCategories());           
            var iemsBLL = new List<CategoryBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(_mapper.Map<CategoryBLL>(item));
            }

            return iemsBLL;
        }

        /// <summary>
        /// Gets category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CategoryBLL> GetCategoryAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.CategorysRepository.ReadCategory(id));
            return _mapper.Map<CategoryBLL>(item);
        }

        /// <summary>
        /// Creates new category
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AddCategoryAsync(CategoryBLL item)
        {
            var itemDAL = _mapper.Map<Category>(item);
            _unitOfWork.CategorysRepository.CreateCategory(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Udates category
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task UpdateCategoryAsync(CategoryBLL item)
        {
            _unitOfWork.CategorysRepository.UpdateCategory(_mapper.Map<Category>(item));
            await _unitOfWork.SaveChangesAsync();
        }
        
        /// <summary>
        /// Delets category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteCategoryAsync(int id)
        {
            _unitOfWork.CategorysRepository.DeleteCategory(id);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Adds post to category
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task AddCategoryToPostAsync(int postId, int categoryId)
        {
            _unitOfWork.PostCategoriesRepository.AddPostToCategory(new PostCategories { PostId = postId, CategoryId = categoryId });
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
