using BLL.Models;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using BLL.ExtensionsForTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.Interfaces;
using BLL.Models;

namespace BLL.Services
{
    public class TagService : ITagService<TagBLL>
    {
        private UnitOfWork _unitOfWork;

        public TagService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void Add(TagBLL item)
        {
            var itemDAL = item.Transform();
            _unitOfWork.TagsRepository.Create(itemDAL);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<TagBLL> GetAll()
        {
            var itemsDAL = _unitOfWork.TagsRepository.ReadAll();
            var iemsBLL = new List<TagBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(item.Transform());
            }

            return iemsBLL;
        }

        public TagBLL Get(int id)
        {
            var item = _unitOfWork.TagsRepository.Read(id);
            if (item != null)
            {
                return item.Transform();
            }
            else return null;
           
        }

        public void Update(TagBLL item)
        {
            _unitOfWork.TagsRepository.Update(item.Id, item.Transform());
            _unitOfWork.SaveChanges();
        }
        
        public void Delete(int id)
        {
            _unitOfWork.TagsRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }

    }
}
