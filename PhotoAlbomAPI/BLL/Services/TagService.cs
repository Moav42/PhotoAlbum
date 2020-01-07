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

        /// <summary>
        /// Creates Tag
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AddTagAsync(TagBLL item)
        {
            var itemDAL = _mapper.Map<TagBLL, Tag>(item);
            _unitOfWork.TagsRepository.CreateTag(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all tags
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TagBLL>> GetAllTagsAsync()
        {
            var itemsDAL =  await Task.Run(() =>_unitOfWork.TagsRepository.ReadAllTags());
            var iemsBLL = new List<TagBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(_mapper.Map<TagBLL>(item));
            }

            return iemsBLL;
        }

        /// <summary>
        /// Gets tag by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TagBLL> GetTagAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.TagsRepository.ReadTag(id));
            return _mapper.Map<TagBLL>(item);
        }

        /// <summary>
        /// Updates tag
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task UpdateTagAsync(TagBLL item)
        {
            _unitOfWork.TagsRepository.UpdateTag( _mapper.Map<Tag>(item));
            await _unitOfWork.SaveChangesAsync();
        }
        
        /// <summary>
        /// Deletes tag
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteTagAsync(int id)
        {
            _unitOfWork.TagsRepository.DeleteTag(id);
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
            _unitOfWork.PostTagsRepository.AddTagToPost(new PostTags { PostId = postId, TagId = tagId });
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
