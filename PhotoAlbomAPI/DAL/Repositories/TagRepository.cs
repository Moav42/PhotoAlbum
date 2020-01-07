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
    public class TagRepository : ITagRepository<Tag>
    {
        private readonly DbContext DB;

        /// <summary>
        /// Configure repository by DbContext, provides by UoF
        /// </summary>
        /// <param name="context"></param>
        public TagRepository(DbContext context)
        {
            DB = context;
        }

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="item"></param>
        public void CreateTag(Tag item)
        {
            DB.Tags.Add(item);
        }

        /// <summary>
        /// Delete tag by id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTag(int id)
        {
            var item = DB.Tags.Find(id);
            if (item != null)
            {
                DB.Tags.Remove(item);
            }
        }

        /// <summary>
        /// Gets tag by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tag ReadTag(int id)
        {
            return DB.Tags.Find(id);
        }

        /// <summary>
        /// Gets all tags
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tag> ReadAllTags()
        {
            return DB.Tags;
        }
       
        /// <summary>
        /// Update tag
        /// </summary>
        /// <param name="modeifayModel"></param>
        public void UpdateTag( Tag modeifayModel)
        {

            DB.Update(modeifayModel);
        }

        /// <summary>
        /// Gets all tags of given post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public IEnumerable<Tag> ReadAllTagsByPost(int postId)
        {
            var postTags = DB.PostTags.Where(pt => pt.PostId == postId);
            var tags = new List<Tag>();
            foreach (var item in postTags)
            {
                tags.Add(ReadTag(item.TagId));
            }
            return tags;
        }
    }
}
