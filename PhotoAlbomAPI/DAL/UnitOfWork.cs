using System;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Threading.Tasks;
using DAL.Repositories;

namespace DAL
{
    /// <summary>
    /// Represents a standard implementation of a pattern of UnitOfWork. 
    /// Aggregates all repositories, and configures them with a database context, to make sure all repositories use the same context.
    /// Contains properties for each application repository, methods for saving changes to the database, and implementation of Disposable pattern.
    /// </summary>
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

        private readonly DbContext _context;

        /// <summary>
        /// Configures Unit Of Work by DbContext using DI
        /// </summary>
        /// <param name="db"></param>
        public UnitOfWork(DbContext db)
        {
            _context = db;
        }
   
        public ICategoryRepository<Category> CategorysRepository
        {
            get
            {
                if (_categorysRepository == null)
                    _categorysRepository = new CategoryRepository(_context);
                return _categorysRepository;
            }
        }

        public ICommentRepository<Comment> CommentsRepository
        {
            get
            {
                if (_commentsRepository == null)
                    _commentsRepository = new CommentRepository(_context);
                return _commentsRepository;
            }
        }

        public IOrganisationRepository<Organisation> OrganisationsRepository
        {
            get
            {
                if (_organisationsRepository == null)
                    _organisationsRepository = new OrganisationRepository(_context);
                return _organisationsRepository;
            }
        }

        public IPostCategoriesRepository<PostCategories> PostCategoriesRepository
        {
            get
            {
                if (_postCategoriesRepository == null)
                    _postCategoriesRepository = new PostCategoriesRepository(_context);
                return _postCategoriesRepository;
            }
        }

        public IPostRateRepository<PostRate> PostRateRepository
        {
            get
            {
                if (_postRateRepository == null)
                    _postRateRepository = new PostRateRepository(_context);
                return _postRateRepository;
            }
        }

        public IPostRepository<Post> PostsRepository
        {
            get
            {
                if (_postsRepository == null)
                    _postsRepository = new PostRepository(_context);
                return _postsRepository;
            }
        }

        public IPostTagsRepository<PostTags> PostTagsRepository
        {
            get
            {
                if (_postTagsRepository == null)
                    _postTagsRepository = new PostTagsRepository(_context);
                return _postTagsRepository;
            }
        }

        public ITagRepository<Tag> TagsRepository
        {
            get
            {
                if (_tagsRepository == null)
                    _tagsRepository = new TagRepository(_context);
                return _tagsRepository;
            }
        }

        /// <summary>
        /// Saved changes in the database asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Saved changes in the database
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    
    }
}
