using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    /// <summary>
    /// Represents an implementation of a repository pattern for the corresponding entity
    /// </summary>
    public class OrganisationRepository : IOrganisationRepository<Organisation>
    {
        private readonly DbContext DB;

        /// <summary>
        /// Configure repository by DbContext, provides by UoF
        /// </summary>
        /// <param name="context"></param>
        public OrganisationRepository(DbContext context)
        {
            DB = context;
        }
        /// <summary>
        /// Gets Organisation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Organisation ReadOrganisation(int id)
        {
            return DB.Organisations.Find(id);
        }

        /// <summary>
        /// Gets all Organisations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Organisation> ReadAllOrganisations()
        {
            return DB.Organisations;
        }

        /// <summary>
        /// Creates new Organisation
        /// </summary>
        /// <param name="item"></param>
        public void CreateOrganisation(Organisation item)
        {
            DB.Organisations.Add(item);
        }

        /// <summary>
        /// Updates Organisation
        /// </summary>
        /// <param name="item"></param>
        public void UpdateOrganisation(Organisation item)
        {
            DB.Organisations.Update(item);
        }

        /// <summary>
        /// Deletes Organisation by id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOrganisation(int id)
        {
            var item = DB.Organisations.Find(id);
            if (item != null)
            {
                DB.Organisations.Remove(item);
            }
        }
    }
}
