using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IOrganisationRepository<T>
    {
        IEnumerable<T> ReadAll();
        T Read(int id);
        void Create(T item);
        void Update(int id, T item);
        void Delete(int id);
    }
}
