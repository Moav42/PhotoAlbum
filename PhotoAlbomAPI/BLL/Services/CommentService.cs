using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Interfaces;
using BLL.ExtensionsForTransfer;

namespace BLL.Services
{
    public class CommentService
    {
        private UnitOfWork _unitOfWork;
        public CommentService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public IEnumerable<CommentBLL> GetByPost(int postId)
        {
            var itemsDAL = _unitOfWork.CommentsRepository.ReadByPost(postId);
            var iemsBLL = new List<CommentBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(item.Transform());
            }

            return iemsBLL;
        }
        public IEnumerable<CommentBLL> GetByUser(string userId)
        {
            var itemsDAL = _unitOfWork.CommentsRepository.ReadByUser(userId);
            var iemsBLL = new List<CommentBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(item.Transform());
            }

            return iemsBLL;
        }

        public CommentBLL Get(int id)
        {
            var ietm = _unitOfWork.CommentsRepository.Read(id);
            return ietm.Transform();
        }

        public void Add(CommentBLL item)
        {
            var itemDAL = item.Transform();
            _unitOfWork.CommentsRepository.Create(itemDAL);
            _unitOfWork.SaveChanges();
        }

        public void Update(CommentBLL item)
        {
            _unitOfWork.CommentsRepository.Update(item.Transform());
            _unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            _unitOfWork.CommentsRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }
    }
}
