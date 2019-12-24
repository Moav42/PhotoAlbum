using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using BLL.ExtensionsForTransfer;
using BLL.Interfaces;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService<CategoryBLL>
    {
        private readonly UnitOfWork _unitOfWork;
        public CategoryService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<IEnumerable<CategoryBLL>> GetAllAsync()
        {
            var itemsDAL = await Task.Run(() => _unitOfWork.CategorysRepository.ReadAll());
            var iemsBLL = new List<CategoryBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(item.Transform());
            }

            return iemsBLL;
        }

        public async Task<CategoryBLL> GetAsync(int id)
        {
            var ietm = await Task.Run(() => _unitOfWork.CategorysRepository.Read(id));
            return ietm.Transform();
        }

        public async Task AddAsync(CategoryBLL item)
        {
            var itemDAL = item.Transform();
            _unitOfWork.CategorysRepository.Create(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryBLL item)
        {
            _unitOfWork.CategorysRepository.Update(item.Id, item.Transform());
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.CategorysRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
