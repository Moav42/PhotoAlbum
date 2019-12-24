using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Interfaces;
using BLL.ExtensionsForTransfer;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CommentService : ICommentService<CommentBLL>
    {
        private readonly UnitOfWork _unitOfWork;
        public CommentService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public async Task<IEnumerable<CommentBLL>> GetByPostAsync(int postId)
        {
            var itemsDAL = await Task.Run(() => _unitOfWork.CommentsRepository.ReadByPost(postId));
            var iemsBLL = new List<CommentBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(item.Transform());
            }

            return iemsBLL;
        }
        public async Task<IEnumerable<CommentBLL>> GetByUserAsync(string userId)
        {
            var itemsDAL = await Task.Run(() => _unitOfWork.CommentsRepository.ReadByUser(userId));
            var iemsBLL = new List<CommentBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(item.Transform());
            }

            return iemsBLL;
        }

        public async Task<CommentBLL> GetAsync(int id)
        {
            var ietm = await Task.Run(() => _unitOfWork.CommentsRepository.Read(id));
            return ietm.Transform();
        }

        public async Task AddAsync(CommentBLL item)
        {
            var itemDAL = item.Transform();
            _unitOfWork.CommentsRepository.Create(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(CommentBLL item)
        {
            _unitOfWork.CommentsRepository.Update(item.Transform());
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.CommentsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
