using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class PostTagsRepository : IPostTagsRepository<PostTags>
    {
        private readonly DbContext DB;

        public PostTagsRepository(DbContext context)
        {
            DB = context;
        }

        public void AddTagToPost(PostTags item)
        {
            DB.PostTags.Add(item);
        }

        public void DeleteTagFromPost(PostTags item)
        {
            DB.PostTags.Remove(item);
        }
     
    }
}
