using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class OrganisationRepository : IOrganisationRepository<Organisation>
    {
        private DbContext DB;
        public OrganisationRepository(DbContext context)
        {
            DB = context;
        }
        public void Create(Organisation item)
        {
            DB.Organisations.Add(item);
        }
    }
}
