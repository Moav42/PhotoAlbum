using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    class OrganisationRepository : IOrganisationRepository<Organisation>
    {
        private DbContext DB;
        public PostCategoriesRepository(DbContext context)
        {
            DB = context;
        }
    }
}
