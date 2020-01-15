using System.Collections.Generic;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Interfaces;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL.Services
{
    public class CategoryService : ICategoryService<CategoryBLL>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

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

        public async Task<CategoryBLL> GetCategoryAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.CategorysRepository.ReadCategory(id));
            return _mapper.Map<CategoryBLL>(item);
        }

        public async Task AddCategoryAsync(CategoryBLL item)
        {
            var itemDAL = _mapper.Map<Category>(item);
            _unitOfWork.CategorysRepository.CreateCategory(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(CategoryBLL item)
        {
            _unitOfWork.CategorysRepository.UpdateCategory(_mapper.Map<Category>(item));
            await _unitOfWork.SaveChangesAsync();
        }
        
        public async Task DeleteCategoryAsync(int id)
        {
            _unitOfWork.CategorysRepository.DeleteCategory(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddCategoryToPostAsync(int postId, int categoryId)
        {
            _unitOfWork.PostCategoriesRepository.AddPostToCategory(new PostCategories { PostId = postId, CategoryId = categoryId });
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
