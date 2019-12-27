using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IOrganisationRepository<T>
    {
        void Create(T item);
    }
}
