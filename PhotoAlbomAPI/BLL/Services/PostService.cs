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

        /// <summary>
        /// Creates new post
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AddPostAsync(PostBLL item)
        {
            var itemDAL = _mapper.Map<Post>(item);
            itemDAL.AddingDate = DateTime.Now;
            _unitOfWork.PostsRepository.CreatePost(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all posts
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PostBLL>> GetAllPostsAsync()
        {
            var itemsDAL = await Task.Run(() =>_unitOfWork.PostsRepository.ReadAllPosts());
            var iemsBLL = new List<PostBLL>();

            foreach (var item in itemsDAL)
            {
                iemsBLL.Add(_mapper.Map<PostBLL>(item));
            }

            return iemsBLL;
        }

        /// <summary>
        /// Gets post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PostBLL> GetPostAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.PostsRepository.ReadPost(id));
            return _mapper.Map<PostBLL>(item);
        }


        /// <summary>
        /// Gets path for post image by post id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetImagePathByIdAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.PostsRepository.ReadPost(id));
            if(item != null)
            {
                return item.LocationPath;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Updates post
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task UpdatePostAsync(PostBLL item)
        {
            _unitOfWork.PostsRepository.UpdatePost(_mapper.Map<Post>(item));
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeletePostAsync(int id)
        {
            _unitOfWork.PostsRepository.DeletePost(id);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all comments of given post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CommentBLL>> GetAllPostCommentsAsync(int postId)
        {
            var itemDAL = await Task.Run(() => _unitOfWork.CommentsRepository.ReadAllCommentsByPost(postId));
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
        public async Task<IEnumerable<TagBLL>> GetAllPostTagsAsync(int postId)
        {
            var itemDAL = await Task.Run(() => _unitOfWork.TagsRepository.ReadAllTagsByPost(postId));
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
        public async Task<IEnumerable<PostBLL>> GetAllPostsByCategoryAsync(int categoryId)
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
