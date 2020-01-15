using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class TagRepository : ITagRepository<Tag>
    {
        private readonly DbContext DB;

        public TagRepository(DbContext context)
        {
            DB = context;
        }

        public void CreateTag(Tag item)
        {
            DB.Tags.Add(item);
        }

        public void DeleteTag(int id)
        {
            var item = DB.Tags.Find(id);
            if (item != null)
            {
                DB.Tags.Remove(item);
            }
        }

        public Tag ReadTag(int id)
        {
            return DB.Tags.Find(id);
        }

        public IEnumerable<Tag> ReadAllTags()
        {
            return DB.Tags;
        }
       
        public void UpdateTag( Tag modeifayModel)
        {

            DB.Update(modeifayModel);
        }

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
