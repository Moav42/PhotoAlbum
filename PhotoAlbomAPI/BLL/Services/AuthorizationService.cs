using BLL.Interfaces;
using BLL.JWT;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthorizationService : IAuthorizationService<UserBLL>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthorizationService(UserManager<DAL.Entities.User> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<string> GetJWT(string name, string password)
        {
            var identity = await GetClaimsIdentity(name, password);

            if (identity == null)
            {
                return null;
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, name, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return jwt;
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            var role = await _userManager.GetRolesAsync(userToVerify);
            var roleStr = string.Empty;

            foreach (var item in role)
            {
                roleStr += item;
            }

            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id, roleStr));
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
