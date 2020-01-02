using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ITagRepository<T>
    {
        IEnumerable<T> ReadAll();
        T Read(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        public IEnumerable<T> ReadAllByPost(int postId);
        public void AddTagToPost(int tagId, int postId);       
        public void DeleteTagFromPost(int tagId, int postId);

    }
}
