using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// A class representing a service for managing application users accounts
    /// </summary>
    public class AccountManagerService : IAccountManagerService<UserBLL>
    {
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Configures the service with the parameters provided by the dependency injection system
        /// </summary>
        /// <param name="userManager"></param>
        public AccountManagerService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserProfile>> GetAllUsers()
        {
            List<UserProfile> userViewModels = await Task.Run(() =>
               (from user in _userManager.Users
                select new UserProfile
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email

                }).ToList());

            return userViewModels;
        }

        /// <summary>
        /// Creates new account of givven role.
        /// Returns result of operation
        /// </summary>
        /// <param name="email">Email of creeating account</param>
        /// <param name="password">Password of creeating account</param>
        /// <param name="role">Role of creeating account</param>
        /// <returns></returns>
        public async Task<IdentityResult> CreacteAccount(string email, string password, string role)
        {
            var userIdentity = new User { Email = email, UserName = email };

            var result = await _userManager.CreateAsync(userIdentity, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userIdentity, role);
            }
            return result;
        }

        /// <summary>
        /// Edits user account.
        /// Returns result of operation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newName"></param>
        /// <param name="newEmail"></param>
        /// <returns></returns>
        public async Task<IdentityResult> EditUserAccount( string id, string newName, string newEmail)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.UserName = newName;
                user.Email = newEmail;

                var result = await _userManager.UpdateAsync(user);

                return result;
            }
            return IdentityResult.Failed();
        }

        /// <summary>
        /// Delets user account.
        /// Returns result of operation
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IdentityResult> DeleteAccount(string name)
        {
            User user = await _userManager.FindByNameAsync(name);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                return result;
            }
            return IdentityResult.Failed();
        }
    }
}
