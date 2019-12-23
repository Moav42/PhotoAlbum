using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Linq;

namespace DAL.Repositories
{
    public class TagRepository : ITagRepository<Tag>
    {
        private DbContext DB;

        private PostTagsRepository _postTags;
        public TagRepository(DbContext context)
        {
            DB = context;
            _postTags = new PostTagsRepository(context);
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
       
        public void Update(Tag item)
        {
            DB.Tags.Update(item);
        }

        public IEnumerable<Tag> ReadAllByPost(int postId)
        {

            var postTags = _postTags.ReadAll().Where(pt => pt.PostId == postId);
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
            _postTags.Create(postTag);

        }

        public void DeleteTagFromPost(int tagId, int postId)
        {
            var postTag = new PostTags { PostId = postId, TagId = tagId };
            _postTags.Delete(postTag);

        }
    }
}
