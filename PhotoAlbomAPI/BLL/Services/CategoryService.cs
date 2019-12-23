using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using BLL.ExtensionsForTransfer;
using BLL.Interfaces;

namespace BLL.Services
{
    public class CategoryService : ICategoryService<CategoryBLL>
    {
        private UnitOfWork _unitOfWork;
        public CategoryService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public IEnumerable<CategoryBLL> GetAll()
        {
            var itemsDAL = _unitOfWork.CategorysRepository.ReadAll();
            var iemsBLL = new List<CategoryBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(item.Transform());
            }

            return iemsBLL;
        }

        public CategoryBLL Get(int id)
        {
            var ietm = _unitOfWork.CategorysRepository.Read(id);
            return ietm.Transform();
        }

        public void Add(CategoryBLL item)
        {
            var itemDAL = item.Transform();
            _unitOfWork.CategorysRepository.Create(itemDAL);
            _unitOfWork.SaveChanges();
        }

        public void Update(CategoryBLL item)
        {
            _unitOfWork.CategorysRepository.Update(item.Transform());
            _unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            _unitOfWork.CategorysRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }
    }
}
