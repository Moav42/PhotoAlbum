using System.Collections.Generic;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Interfaces;
using System.Threading.Tasks;
using AutoMapper;
using System;

namespace BLL.Services
{
    public class CommentService : ICommentService<CommentBLL>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentBLL>> GetByPostAsync(int postId)
        {
            var itemsDAL = await Task.Run(() => _unitOfWork.CommentsRepository.ReadAllCommentsByPost(postId));
            var iemsBLL = new List<CommentBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(_mapper.Map<CommentBLL>(item));
            }

            return iemsBLL;
        }

        public async Task<IEnumerable<CommentBLL>> GetAllCommentsByUserAsync(string userId)
        {
            var itemsDAL = await Task.Run(() => _unitOfWork.CommentsRepository.ReaAllCommentsdByUser(userId));
            var iemsBLL = new List<CommentBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(_mapper.Map<CommentBLL>(item));
            }

            return iemsBLL;
        }
        public async Task<CommentBLL> GetCommentsAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.CommentsRepository.ReadComment(id));
            return _mapper.Map<CommentBLL>(item);
        }

        public async Task AddCommentAsync(CommentBLL item)
        {
            var itemDAL = _mapper.Map<Comment>(item);
            itemDAL.AddingDate = DateTime.Now;
            _unitOfWork.CommentsRepository.CreateComment(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(CommentBLL item)
        {
            _unitOfWork.CommentsRepository.UpdateComment(_mapper.Map<Comment>(item));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int id)
        {
            _unitOfWork.CommentsRepository.DeleteComment(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
