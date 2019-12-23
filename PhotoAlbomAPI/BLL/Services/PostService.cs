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
    public class PostService
    {
        private UnitOfWork _unitOfWork;

        public PostService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public void Add(PostBLL item)
        {
            var itemDAL = item.Transform();
            _unitOfWork.PostsRepository.Create(itemDAL);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<PostBLL> GetAll()
        {
            var itemsDAL = _unitOfWork.PostsRepository.ReadAll();
            var iemsBLL = new List<PostBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(item.Transform());
            }

            return iemsBLL;
        }

        public PostBLL Get(int id)
        {
            var item = _unitOfWork.PostsRepository.Read(id);
            return item.Transform();
        }

        public void Update(PostBLL item)
        {
            _unitOfWork.PostsRepository.Update(item.Transform());
            _unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            _unitOfWork.PostsRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<CommentBLL> GetComments(int postId)
        {
            var itemDAL = _unitOfWork.CommentsRepository.ReadByPost(postId);
            var itemBLL = new List<CommentBLL>();
            foreach (var item in itemDAL)
            {
                itemBLL.Add(item.Transform());
            }
            return itemBLL;
        }
        public IEnumerable<CategoryBLL> GetCategories(int postId)
        {
            var itemDAL = _unitOfWork.CategorysRepository.ReadAllByPost(postId);
            var itemBLL = new List<CategoryBLL>();
            foreach (var item in itemDAL)
            {
                itemBLL.Add(item.Transform());
            }
            return itemBLL;
        }
        public IEnumerable<TagBLL> GetTags(int postId)
        {
            var itemDAL = _unitOfWork.TagsRepository.ReadAllByPost(postId);
            var itemBLL = new List<TagBLL>();
            foreach (var item in itemDAL)
            {
                itemBLL.Add(item.Transform());
            }
            return itemBLL;
        }
        public IEnumerable<PostRateBLL> GetRates(int postId)
        {
            var itemDAL = _unitOfWork.PostRateRepository.ReadAllByPost(postId);
            var itemBLL = new List<PostRateBLL>();
            foreach (var item in itemDAL)
            {
                itemBLL.Add(item.Transform());
            }
            return itemBLL;
        }

    }
}
