using System.Collections.Generic;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Interfaces;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL.Services
{
    public class PostRateService : IPostRateService<PostRateBLL>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostRateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddRateToPostAsync(PostRateBLL item)
        {
            var itemDAL = _mapper.Map<PostRate>(item);
            _unitOfWork.PostRateRepository.AddRateToPost(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdatePostRateAsync(PostRateBLL item)
        {
            _unitOfWork.PostRateRepository.UpdatePostRate(_mapper.Map<PostRate>(item));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> GetPostRate(int postId, string userId)
        {
            return await Task.Run(() => _unitOfWork.PostRateRepository.GetPostsRate(postId, userId));
        }
    }
}
