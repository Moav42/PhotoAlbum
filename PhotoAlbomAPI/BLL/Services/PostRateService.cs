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

        public async Task AddAsync(PostRateBLL item)
        {
            var itemDAL = _mapper.Map<PostRate>(item);
            _unitOfWork.PostRateRepository.Create(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostRateBLL>> GetAllByPostAsync(int postId)
        {
            var itemDAL = await Task.Run(() =>_unitOfWork.PostRateRepository.ReadAllByPost(postId));
            var itemBLL = new List<PostRateBLL>();
            foreach (var item in itemDAL)
            {
                itemBLL.Add(_mapper.Map<PostRateBLL>(item));
            }
            return itemBLL;
        }

        public async Task<IEnumerable<PostRateBLL>> GetAllByUserAsync(string usertId)
        {
            var itemDAL = await Task.Run(() => _unitOfWork.PostRateRepository.ReadAllByUser(usertId));
            var itemBLL = new List<PostRateBLL>();
            foreach (var item in itemDAL)
            {
                itemBLL.Add(_mapper.Map<PostRateBLL>(item));
            }
            return itemBLL;
        }

        public async Task UpdateAsync(PostRateBLL item)
        {
            _unitOfWork.PostRateRepository.Update(_mapper.Map<PostRate>(item));
            await _unitOfWork.SaveChangesAsync();
        } 

        public async Task<bool> GetPostRate(int postId, string userId)
        {
            return await Task.Run(() => _unitOfWork.PostRateRepository.GetPostsRate(postId, userId));
        }
    }
}
