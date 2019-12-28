using BLL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccountManagerService<T>
    {
        Task<IEnumerable<UserProfile>> GetAllUsers();
        Task<IdentityResult> CreacteAccount(string email, string password, string role);
        Task<IdentityResult> EditUserAccount(string userName, string email);
        Task<IdentityResult> DeleteAccount(string name);
    }
}
