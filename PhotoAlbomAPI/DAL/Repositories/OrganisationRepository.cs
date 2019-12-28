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
        private readonly DbContext DB;
        public OrganisationRepository(DbContext context)
        {
            DB = context;
        }
        public Organisation Read(int id)
        {
            return DB.Organisations.Find(id);
        }

        public IEnumerable<Organisation> ReadAll()
        {
            return DB.Organisations;
        }

        public void Create(Organisation item)
        {
            DB.Organisations.Add(item);
        }

        public void Update(int id, Organisation item)
        {
            var model = Read(id);
            model.Name = item.Name;
            model.Location = item.Location;

            DB.Organisations.Update(model);
        }

        public void Delete(int id)
        {
            var item = DB.Organisations.Find(id);
            if (item != null)
            {
                DB.Organisations.Remove(item);
            }
        }
    }
}
