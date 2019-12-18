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

        private readonly DbContext DataBase;
        public UnitOfWork()
        {
            DataBase = new DbContext();
        }

        public ICategoryRepository<Category> CategorysRepository
        {
            get
            {
                if (_categorysRepository == null)
                    _categorysRepository = new CategoryRepository(DataBase);
                return _categorysRepository;
            }
        }

        public ICommentRepository<Comment> CommentsRepository
        {
            get
            {
                if (_commentsRepository == null)
                    _commentsRepository = new CommentRepository(DataBase);
                return _commentsRepository;
            }
        }

        public IOrganisationRepository<Organisation> OrganisationsRepository
        {
            get
            {
                if (_organisationsRepository == null)
                    _organisationsRepository = new OrganisationRepository(DataBase);
                return _organisationsRepository;
            }
        }

        public IPostCategoriesRepository<PostCategories> PostCategoriesRepository
        {
            get
            {
                if (_postCategoriesRepository == null)
                    _postCategoriesRepository = new PostCategoriesRepository(DataBase);
                return _postCategoriesRepository;
            }
        }

        public IPostRateRepository<PostRate> PostRateRepository
        {
            get
            {
                if (_postRateRepository == null)
                    _postRateRepository = new PostRateRepository(DataBase);
                return _postRateRepository;
            }
        }

        public IPostRepository<Post> PostsRepository
        {
            get
            {
                if (_postsRepository == null)
                    _postsRepository = new PostRepository(DataBase);
                return _postsRepository;
            }
        }

        public IPostTagsRepository<PostTags> PostTagsRepository
        {
            get
            {
                if (_postTagsRepository == null)
                    _postTagsRepository = new PostTagsRepository(DataBase);
                return _postTagsRepository;
            }
        }

        public ITagRepository<Tag> TagsRepository
        {
            get
            {
                if (_tagsRepository == null)
                    _tagsRepository = new TagRepository(DataBase);
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
                    DataBase.Dispose();
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
            return await DataBase.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            DataBase.SaveChanges();
        }
    }
}
