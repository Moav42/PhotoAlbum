using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using BLL.Interfaces;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL.Services
{
    /// <summary>
    /// A service containing business logic that is responsible for a specific resource. Configurable by the UoF, implemented through the DI
    /// </summary>
    public class TagService : ITagService<TagBLL>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Configures the service with the parameters provided by the dependency injection system
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(TagBLL item)
        {
            var itemDAL = _mapper.Map<TagBLL, Tag>(item);
            _unitOfWork.TagsRepository.Create(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TagBLL>> GetAllAsync()
        {
            var itemsDAL =  await Task.Run(() =>_unitOfWork.TagsRepository.ReadAll());
            var iemsBLL = new List<TagBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(_mapper.Map<TagBLL>(item));
            }

            return iemsBLL;
        }

        public async Task<TagBLL> GetAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.TagsRepository.Read(id));
            return _mapper.Map<TagBLL>(item);
        }

        public TagBLL Get(int id)
        {
            var item = _unitOfWork.TagsRepository.Read(id);
            return _mapper.Map<TagBLL>(item);
        }

        public async Task UpdateAsync(TagBLL item)
        {
            _unitOfWork.TagsRepository.Update( _mapper.Map<Tag>(item));
            await _unitOfWork.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            _unitOfWork.TagsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Adds tag to post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public async Task AddTagToPostAsync(int postId, int tagId)
        {
            _unitOfWork.PostTagsRepository.Create(new PostTags { PostId = postId, TagId = tagId });
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
