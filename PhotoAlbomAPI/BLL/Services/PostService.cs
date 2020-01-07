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
    public class PostService : IPostService<PostBLL>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Configures the service with the parameters provided by the dependency injection system
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
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

        public PostBLL Get(int id)
        {
            var item = _unitOfWork.PostsRepository.Read(id);
            return _mapper.Map<PostBLL>(item);
        }

        /// <summary>
        /// Gets path for post image by post id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets all comments of given post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets all tags of given post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets all category of given post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
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
