using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using BLL.ExtensionsForTransfer;
using BLL.Interfaces;
using DAL.Repositories;

namespace BLL.Services
{
    public class PostRateService : IPostRateService<PostRateBLL>
    {
        private UnitOfWork _unitOfWork;

        public PostRateService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public void Add(PostRateBLL item)
        {
            var itemDAL = item.Transform();
            _unitOfWork.PostRateRepository.Create(itemDAL);
            _unitOfWork.SaveChanges();
        }


        public IEnumerable<PostRateBLL> GetAllByPost(int postId)
        {
            var itemDAL = _unitOfWork.PostRateRepository.ReadAllByPost(postId);
            var itemBLL = new List<PostRateBLL>();
            foreach (var item in itemDAL)
            {
                itemBLL.Add(item.Transform());
            }
            return itemBLL;
        }

        public void Update(PostRateBLL item)
        {
            _unitOfWork.PostRateRepository.Update(item.Transform());
            _unitOfWork.SaveChanges();
        }
    }
}
