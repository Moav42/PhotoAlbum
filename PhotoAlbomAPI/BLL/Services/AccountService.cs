using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// A class representing a service for registering a user and changing his password
    /// </summary>
    public class AccountService : IAccountService<UserBLL>
    {
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Configures the service with the parameters provided by the dependency injection system
        /// </summary>
        /// <param name="userManager"></param>
        public AccountService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Creates new user account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> RegisterUser(UserBLL model)
        {
            var userIdentity = new User { Email = model.Email, UserName = model.Email };
            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userIdentity, "user");
                return result;
            }
            return result;
        }

        /// <summary>
        /// Changes user passwrod
        /// </summary>
        /// <param name="name">Name of user</param>
        /// <param name="oldPass">Old user password</param>
        /// <param name="newPass">New user password</param>
        /// <returns></returns>
        public async Task<IdentityResult> ChangePassword(string name, string oldPass, string newPass)
        {
            User user = await _userManager.FindByNameAsync(name);

            if (user == null)
            {
                return IdentityResult.Failed();
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(user, oldPass, newPass);

            return result;
        }
    }
}
