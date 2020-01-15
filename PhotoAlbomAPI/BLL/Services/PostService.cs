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

        public async Task AddPostAsync(PostBLL item)
        {
            var itemDAL = _mapper.Map<Post>(item);
            itemDAL.AddingDate = DateTime.Now;
            _unitOfWork.PostsRepository.CreatePost(itemDAL);
            await _unitOfWork.SaveChangesAsync();
        }

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

        public async Task<PostBLL> GetPostAsync(int id)
        {
            var item = await Task.Run(() => _unitOfWork.PostsRepository.ReadPost(id));
            return _mapper.Map<PostBLL>(item);
        }


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

        public async Task UpdatePostAsync(PostBLL item)
        {
            _unitOfWork.PostsRepository.UpdatePost(_mapper.Map<Post>(item));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            _unitOfWork.PostsRepository.DeletePost(id);
            await _unitOfWork.SaveChangesAsync();
        }

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
