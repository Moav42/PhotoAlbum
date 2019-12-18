using BLL.Models;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using BLL.ExtensionsForTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class TagService
    {
        private UnitOfWork _unitOfWork;

        public TagService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public void AddTag(TagBLL item)
        {
            var tag = item.Transform();
            _unitOfWork.TagsRepository.Create(tag);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<TagBLL> GetTags()
        {
            var tags = _unitOfWork.TagsRepository.ReadAll();
            var tagsBll = new List<TagBLL>();

            foreach (var item in tags)
            {
                tagsBll.Add(item.Transform());
            }

            return tagsBll;
        }
        public TagBLL GetTag(int id)
        {
            var tags = _unitOfWork.TagsRepository.Read(id);
            return tags.Transform();

        }
    }
}
