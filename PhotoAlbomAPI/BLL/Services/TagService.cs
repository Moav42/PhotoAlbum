using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using BLL.Interfaces;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL.Services
{
    public class TagService : ITagService<TagBLL>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddTagAsync(TagBLL item)
        {
            var itemDAL = _mapper.Map<TagBLL, Tag>(item);
            _unitOfWork.TagsRepository.CreateTag(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

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

        public async Task<TagBLL> GetTagAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.TagsRepository.ReadTag(id));
            return _mapper.Map<TagBLL>(item);
        }

        public async Task UpdateTagAsync(TagBLL item)
        {
            _unitOfWork.TagsRepository.UpdateTag( _mapper.Map<Tag>(item));
            await _unitOfWork.SaveChangesAsync();
        }
        
        public async Task DeleteTagAsync(int id)
        {
            _unitOfWork.TagsRepository.DeleteTag(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddTagToPostAsync(int postId, int tagId)
        {
            _unitOfWork.PostTagsRepository.AddTagToPost(new PostTags { PostId = postId, TagId = tagId });
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
