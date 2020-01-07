using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IOrganisationRepository<T>
    {
        IEnumerable<T> ReadAllOrganisations();
        T ReadOrganisation(int id);
        void CreateOrganisation(T item);
        void UpdateOrganisation(T item);
        void DeleteOrganisation(int id);
    }
}
