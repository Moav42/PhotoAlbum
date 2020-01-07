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
    /// <summary>
    /// A service containing business logic that is responsible for a specific resource. Configurable by the UoF, implemented through the DI
    /// </summary>
    public class CommentService : ICommentService<CommentBLL>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Configures the service with the parameters provided by the dependency injection system
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all cooments of given post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CommentBLL>> GetByPostAsync(int postId)
        {
            var itemsDAL = await Task.Run(() => _unitOfWork.CommentsRepository.ReadByPost(postId));
            var iemsBLL = new List<CommentBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(_mapper.Map<CommentBLL>(item));
            }

            return iemsBLL;
        }

        /// <summary>
        /// Gets all comments of givn user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CommentBLL>> GetByUserAsync(string userId)
        {
            var itemsDAL = await Task.Run(() => _unitOfWork.CommentsRepository.ReadByUser(userId));
            var iemsBLL = new List<CommentBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(_mapper.Map<CommentBLL>(item));
            }

            return iemsBLL;
        }

        public async Task<CommentBLL> GetAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.CommentsRepository.Read(id));
            return _mapper.Map<CommentBLL>(item);
        }

        public CommentBLL Get(int id)
        {
            var item = _unitOfWork.CommentsRepository.Read(id);
            return _mapper.Map<CommentBLL>(item);
        }

        public async Task AddAsync(CommentBLL item)
        {
            var itemDAL = _mapper.Map<Comment>(item);
            itemDAL.AddingDate = DateTime.Now;
            _unitOfWork.CommentsRepository.Create(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(CommentBLL item)
        {
            _unitOfWork.CommentsRepository.Update(_mapper.Map<Comment>(item));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.CommentsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
