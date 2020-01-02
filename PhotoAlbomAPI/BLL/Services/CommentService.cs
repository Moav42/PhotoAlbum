using System.Collections.Generic;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Interfaces;
using System.Threading.Tasks;
using AutoMapper;

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
            var itemsDAL = await Task.Run(() => _unitOfWork.CommentsRepository.ReadByPost(postId));
            var iemsBLL = new List<CommentBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(_mapper.Map<CommentBLL>(item));
            }

            return iemsBLL;
        }

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

        public async Task AddAsync(CommentBLL item)
        {
            var itemDAL = _mapper.Map<Comment>(item);
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
