using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountService : IAccountService<UserBLL>
    {
        private readonly UserManager<User> _userManager;

        public AccountService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

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
