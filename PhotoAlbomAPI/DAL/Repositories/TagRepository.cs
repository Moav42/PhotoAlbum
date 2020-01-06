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

        public void Create(Tag item)
        {
            DB.Tags.Add(item);
        }

        public void Delete(int id)
        {
            var item = DB.Tags.Find(id);
            if (item != null)
            {
                DB.Tags.Remove(item);
            }
        }

        public Tag Read(int id)
        {
            return DB.Tags.Find(id);
        }

        public IEnumerable<Tag> ReadAll()
        {
            return DB.Tags;
        }
       
        public void Update( Tag modeifayModel)
        {

            DB.Update(modeifayModel);
        }

        public IEnumerable<Tag> ReadAllByPost(int postId)
        {
            var postTags = DB.PostTags.Where(pt => pt.PostId == postId);
            var tags = new List<Tag>();
            foreach (var item in postTags)
            {
                tags.Add(Read(item.TagId));
            }
            return tags;
        }

        public void AddTagToPost(int tagId, int postId)
        {
            var postTag = new PostTags { PostId = postId, TagId = tagId };
            DB.PostTags.Add(postTag);
        }

        public void DeleteTagFromPost(int tagId, int postId)
        {
            var postTag = new PostTags { PostId = postId, TagId = tagId };
            DB.PostTags.Remove(postTag);
        }
    }
}
