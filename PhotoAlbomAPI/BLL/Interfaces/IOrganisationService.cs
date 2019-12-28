using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrganisationService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<IdentityResult> RegisterOrganisation(T model);
        Task UpdateAsync(T model);
        Task DeleteAsync(int id);
    }
}