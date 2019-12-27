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
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PostRateService : IPostRateService<PostRateBLL>
    {
        private readonly UnitOfWork _unitOfWork;

        public PostRateService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task AddAsync(PostRateBLL item)
        {
            var itemDAL = item.Transform();
            _unitOfWork.PostRateRepository.Create(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostRateBLL>> GetAllByPostAsync(int postId)
        {
            var itemDAL = await Task.Run(() =>_unitOfWork.PostRateRepository.ReadAllByPost(postId));
            var itemBLL = new List<PostRateBLL>();
            foreach (var item in itemDAL)
            {
                itemBLL.Add(item.Transform());
            }
            return itemBLL;
        }

        public async Task UpdateAsync(PostRateBLL item)
        {
            _unitOfWork.PostRateRepository.Update(item.Transform());
            await _unitOfWork.SaveChangesAsync();
        } 
    }
}
