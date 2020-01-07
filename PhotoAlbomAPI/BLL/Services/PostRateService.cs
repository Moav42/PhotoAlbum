using System.Collections.Generic;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Interfaces;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL.Services
{
    /// <summary>
    /// A service containing business logic that is responsible for a specific resource. Configurable by the UoF, implemented through the DI
    /// </summary>
    public class PostRateService : IPostRateService<PostRateBLL>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Configures the service with the parameters provided by the dependency injection system
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public PostRateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(PostRateBLL item)
        {
            var itemDAL = _mapper.Map<PostRate>(item);
            _unitOfWork.PostRateRepository.Create(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(PostRateBLL item)
        {
            _unitOfWork.PostRateRepository.Update(_mapper.Map<PostRate>(item));
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Determines if a user rated a post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> GetPostRate(int postId, string userId)
        {
            return await Task.Run(() => _unitOfWork.PostRateRepository.GetPostsRate(postId, userId));
        }
    }
}
