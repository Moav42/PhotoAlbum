using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountManagerService : IAccountManagerService<UserBLL>
    {
        private readonly UserManager<User> _userManager;

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

        public async Task<IdentityResult> EditUserAccount( string oldName, string newName)
        {
            User user = await _userManager.FindByNameAsync(oldName);
            if (user != null)
            {
                user.UserName = newName;

                var result = await _userManager.UpdateAsync(user);

                return result;
            }
            return IdentityResult.Failed();
        }

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
