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
    public class PostService : IPostService<PostBLL>
    {
        private readonly UnitOfWork _unitOfWork;

        public PostService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public async Task AddAsync(PostBLL item)
        {
            var itemDAL = item.Transform();
            _unitOfWork.PostsRepository.Create(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostBLL>> GetAllAsync()
        {
            var itemsDAL = await Task.Run(() =>_unitOfWork.PostsRepository.ReadAll());
            var iemsBLL = new List<PostBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(item.Transform());
            }

            return iemsBLL;
        }

        public async Task<PostBLL> GetAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.PostsRepository.Read(id));
            return item.Transform();
        }

        public async Task UpdateAsync(PostBLL item)
        {
            _unitOfWork.PostsRepository.Update(item.Id, item.Transform());
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.PostsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommentBLL>> GetCommentsAsync(int postId)
        {
            var itemDAL = await Task.Run(() => _unitOfWork.CommentsRepository.ReadByPost(postId));
            var itemBLL = new List<CommentBLL>();
            foreach (var item in itemDAL)
            {
                itemBLL.Add(item.Transform());
            }
            return itemBLL;
        }
        public async Task<IEnumerable<CategoryBLL>> GetCategoriesAsync(int postId)
        {
            var itemDAL = await Task.Run(() => _unitOfWork.CategorysRepository.ReadAllByPost(postId));
            var itemBLL = new List<CategoryBLL>();
            foreach (var item in itemDAL)
            {
                itemBLL.Add(item.Transform());
            }
            return itemBLL;
        }
        public async Task<IEnumerable<TagBLL>> GetTagsAsync(int postId)
        {
            var itemDAL = await Task.Run(() => _unitOfWork.TagsRepository.ReadAllByPost(postId));
            var itemBLL = new List<TagBLL>();
            foreach (var item in itemDAL)
            {
                itemBLL.Add(item.Transform());
            }
            return itemBLL;
        }
        public async Task<IEnumerable<PostRateBLL>> GetRatesAsync(int postId)
        {
            var itemDAL = await Task.Run(() => _unitOfWork.PostRateRepository.ReadAllByPost(postId));
            var itemBLL = new List<PostRateBLL>();
            foreach (var item in itemDAL)
            {
                itemBLL.Add(item.Transform());
            }
            return itemBLL;
        }

    }
}
