using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class OrganisationRepository : IOrganisationRepository<Organisation>
    {
        private readonly DbContext DB;

        public OrganisationRepository(DbContext context)
        {
            DB = context;
        }
        public Organisation ReadOrganisation(int id)
        {
            return DB.Organisations.Find(id);
        }

        public IEnumerable<Organisation> ReadAllOrganisations()
        {
            return DB.Organisations;
        }

        public void CreateOrganisation(Organisation item)
        {
            DB.Organisations.Add(item);
        }

        public void UpdateOrganisation(Organisation item)
        {
            DB.Organisations.Update(item);
        }

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
