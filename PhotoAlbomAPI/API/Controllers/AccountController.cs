using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.ViewModels;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager ;      
        private readonly IOrganisationService<OrganisationBLL> _organisationService;

        public AccountController(UserManager<User> userManager, IOrganisationService<OrganisationBLL> organisationService )
        {
            _userManager = userManager;         
            _organisationService = organisationService;
        }

        [HttpPost("reg/user")]
        public async Task<IActionResult> RegisterUser([FromBody]UserRegisterViewModel model)
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
                await _userManager.AddToRoleAsync(userIdentity, "user");
                return new OkObjectResult("User account created");
            }
        }

        [HttpPost("reg/org")]
        public async Task<IActionResult> RegisterOrganisation([FromBody]OrganisationRegisterViewModel model)
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
                await _organisationService.AddAsync(new OrganisationBLL { Location = model.Location, Name = model.Name, UserId = userIdentity.Id });
                await _userManager.AddToRoleAsync(userIdentity, "organisation");
                return new OkObjectResult("Organisation account created");
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordViewModel model)
        {
            User user = await _userManager.FindByNameAsync(model.Name);

            if (user == null)
            {
                return NotFound("Invalid login");
            }

            if (ModelState.IsValid)
            {
                IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return new OkObjectResult("Password changed");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return BadRequest(ModelState);           
        }
    }
}