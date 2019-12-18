using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        ICategoryRepository<Category> CategorysRepository { get; }
            
        ICommentRepository<Comment> CommentsRepository { get; }

        IOrganisationRepository<Organisation> OrganisationsRepository { get; }

        IPostCategoriesRepository<PostCategories> PostCategoriesRepository { get; }

        IPostRateRepository<PostRate> PostRateRepository { get; }

        IPostRepository<Post> PostsRepository { get; }

        IPostTagsRepository<PostTags> PostTagsRepository { get; }

        ITagRepository<Tag> TagsRepository { get; }

        Task<int> SaveChangesAsync();
        void SaveChanges();

    }
}
