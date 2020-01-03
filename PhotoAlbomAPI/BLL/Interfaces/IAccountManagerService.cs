using BLL.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccountManagerService<T>
    {
        Task<IEnumerable<UserProfile>> GetAllUsers();
        Task<IdentityResult> CreacteAccount(string email, string password, string role);
        Task<IdentityResult> EditUserAccount(string id, string newName, string newEmail);
        Task<IdentityResult> DeleteAccount(string name);
    }
}
