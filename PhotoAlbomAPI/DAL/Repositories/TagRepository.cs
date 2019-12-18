using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;

namespace DAL.Repositories
{
    public class TagRepository : ITagRepository<Tag>
    {
        private DbContext DB;
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

        public void Update(Tag item)
        {
            DB.Tags.Update(item);
        }
    }
}
