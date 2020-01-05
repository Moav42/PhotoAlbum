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
    public class PostService : IPostService<PostBLL>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(PostBLL item)
        {
            var itemDAL = _mapper.Map<Post>(item);
            itemDAL.AddingDate = DateTime.Now;
            _unitOfWork.PostsRepository.Create(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostBLL>> GetAllAsync()
        {
            var itemsDAL = await Task.Run(() =>_unitOfWork.PostsRepository.ReadAll());
            var iemsBLL = new List<PostBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(_mapper.Map<PostBLL>(item));
            }

            return iemsBLL;
        }

        public async Task<PostBLL> GetAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.PostsRepository.Read(id));
            return _mapper.Map<PostBLL>(item);
        }

        public async Task<string> GetPathByIdAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.PostsRepository.Read(id));
            if(item != null)
            {
                return item.LocationPath;
            }
            else
            {
                return null;
            }
        }

        public async Task UpdateAsync(PostBLL item)
        {
            _unitOfWork.PostsRepository.Update(_mapper.Map<Post>(item));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.PostsRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommentBLL>> GetCommentsAsync(int postId)
        {
            var itemDAL = await Task.Run(() => _unitOfWork.CommentsRepository.ReadByPost(postId));
            var itemBLL = new List<CommentBLL>();
            foreach (var item in itemDAL)
            {
                itemBLL.Add(_mapper.Map<CommentBLL>(item));
            }
            return itemBLL;
        }

        public async Task<IEnumerable<TagBLL>> GetTagsAsync(int postId)
        {
            var itemDAL = await Task.Run(() => _unitOfWork.TagsRepository.ReadAllByPost(postId));
            var itemBLL = new List<TagBLL>();
            foreach (var item in itemDAL)
            {
                itemBLL.Add(_mapper.Map<TagBLL>(item));
            }
            return itemBLL;
        }
        public async Task<IEnumerable<PostBLL>> GetAllByCategoryAsync(int categoryId)
        {
            var itemsDAL = await Task.Run(() => _unitOfWork.PostsRepository.ReadAllPostsByCategory(categoryId));
            var iemsBLL = new List<PostBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(_mapper.Map<PostBLL>(item));
            }

            return iemsBLL;
        }
    }
}
