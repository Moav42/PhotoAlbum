using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    /// <summary>
    /// Represents an implementation of a repository pattern for the corresponding entity
    /// </summary>
    public class PostTagsRepository : IPostTagsRepository<PostTags>
    {
        private readonly DbContext DB;

        /// <summary>
        /// Configure repository by DbContext, provides by UoF
        /// </summary>
        /// <param name="context"></param>
        public PostTagsRepository(DbContext context)
        {
            DB = context;
        }

        /// <summary>
        /// Adds tag to post
        /// </summary>
        /// <param name="item"></param>
        public void AddTagToPost(PostTags item)
        {
            DB.PostTags.Add(item);
        }

        /// <summary>
        /// Removes tag from post
        /// </summary>
        /// <param name="item"></param>
        public void DeleteTagFromPost(PostTags item)
        {
            DB.PostTags.Remove(item);
        }
     
    }
}
