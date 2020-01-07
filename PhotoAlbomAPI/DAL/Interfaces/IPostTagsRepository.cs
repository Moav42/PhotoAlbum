using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IPostTagsRepository<T>
    {
        void AddTagToPost(T item);
        void DeleteTagFromPost(T item);

    }
}
