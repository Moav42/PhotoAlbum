using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Threading.Tasks;
using DAL.Repositories;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {

        private ICategoryRepository<Category> _categorysRepository;

        private ICommentRepository<Comment> _commentsRepository;

        private IOrganisationRepository<Organisation> _organisationsRepository;

        private IPostCategoriesRepository<PostCategories> _postCategoriesRepository;

        private IPostRateRepository<PostRate> _postRateRepository;

        private IPostRepository<Post> _postsRepository;

        private IPostTagsRepository<PostTags> _postTagsRepository;

        private ITagRepository<Tag> _tagsRepository;

        private readonly DbContext _DataBase;

        public UnitOfWork()
        {
            _DataBase = new DbContext();
        }
        public DbContext Context 
        {
            get
            {
                return _DataBase;
            }
        }
        public ICategoryRepository<Category> CategorysRepository
        {
            get
            {
                if (_categorysRepository == null)
                    _categorysRepository = new CategoryRepository(_DataBase);
                return _categorysRepository;
            }
        }

        public ICommentRepository<Comment> CommentsRepository
        {
            get
            {
                if (_commentsRepository == null)
                    _commentsRepository = new CommentRepository(_DataBase);
                return _commentsRepository;
            }
        }

        public IOrganisationRepository<Organisation> OrganisationsRepository
        {
            get
            {
                if (_organisationsRepository == null)
                    _organisationsRepository = new OrganisationRepository(_DataBase);
                return _organisationsRepository;
            }
        }

        public IPostCategoriesRepository<PostCategories> PostCategoriesRepository
        {
            get
            {
                if (_postCategoriesRepository == null)
                    _postCategoriesRepository = new PostCategoriesRepository(_DataBase);
                return _postCategoriesRepository;
            }
        }

        public IPostRateRepository<PostRate> PostRateRepository
        {
            get
            {
                if (_postRateRepository == null)
                    _postRateRepository = new PostRateRepository(_DataBase);
                return _postRateRepository;
            }
        }

        public IPostRepository<Post> PostsRepository
        {
            get
            {
                if (_postsRepository == null)
                    _postsRepository = new PostRepository(_DataBase);
                return _postsRepository;
            }
        }

        public IPostTagsRepository<PostTags> PostTagsRepository
        {
            get
            {
                if (_postTagsRepository == null)
                    _postTagsRepository = new PostTagsRepository(_DataBase);
                return _postTagsRepository;
            }
        }

        public ITagRepository<Tag> TagsRepository
        {
            get
            {
                if (_tagsRepository == null)
                    _tagsRepository = new TagRepository(_DataBase);
                return _tagsRepository;
            }
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _DataBase.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _DataBase.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _DataBase.SaveChanges();
        }
    }
}
