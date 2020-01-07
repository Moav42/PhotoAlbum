using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ITagRepository<T>
    {
        IEnumerable<T> ReadAllTags();
        T ReadTag(int id);
        void CreateTag(T item);
        void UpdateTag(T item);
        void DeleteTag(int id);
        public IEnumerable<T> ReadAllTagsByPost(int postId);

    }
}
