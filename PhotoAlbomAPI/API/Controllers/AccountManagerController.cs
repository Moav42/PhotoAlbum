using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.ViewModels;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountManagerController : ControllerBase
    {
        private readonly UserManager<DAL.Entities.User> _userManager;
        private readonly IOrganisationService<OrganisationBLL> _organisationService;

        public AccountManagerController(UserManager<User> userManager, IOrganisationService<OrganisationBLL> organisationService)
        {
            _userManager = userManager;
            _organisationService = organisationService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {

            List<UserViewModel> userViewModels = await Task.Run(() => 
            (from user in _userManager.Users 
            select new UserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email

            }).ToList());

            return userViewModels;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreacteAccount([FromBody]AccountCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = new User { Email = model.Email, UserName = model.Email };

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }
            else
            {
                await _userManager.AddToRoleAsync(userIdentity, model.Role);
                return new OkObjectResult("Account created");
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditUserAccount([FromBody] EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return new OkObjectResult("Account updated");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteAccount([FromBody] string name)
        {
            User user = await _userManager.FindByNameAsync(name);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                return new OkObjectResult("Account deleted");
            }
            return NotFound("Invalid user name");
        }
    }
}