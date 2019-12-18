using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using BLL.ExtensionsForTransfer;

namespace BLL.Services
{
    public class PostService
    {
        private UnitOfWork _unitOfWork;
        public PostService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public void AddPost(PostBLL item)
        {
            var post = item.Transform();
            _unitOfWork.PostsRepository.Create(post);
            _unitOfWork.SaveChanges();
        }

    }
}
