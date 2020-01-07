using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrganisationService<T>
    {
        Task<IEnumerable<T>> GetAllOrganisationAccountsAsync();
        Task<T> GetOrganisationAccountAsync(int id);
        Task<IdentityResult> RegisterOrganisation(T model);
        Task UpdateOrganisationAccountAsync(T model);
        Task DeleteOrganisationAccountAsync(int id);
    }
}