using System.Threading.Tasks;
using API.Models.ViewModels;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "AllUsers")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService<UserBLL> _accountService;
        private readonly IOrganisationService<OrganisationBLL> _organisationService;

        public AccountController( IOrganisationService<OrganisationBLL> organisationService, IAccountService<UserBLL> accountService )
        {      
            _organisationService = organisationService;
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("reg/user")]
        public async Task<IActionResult> RegisterUser([FromBody]UserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountService.RegisterUser(new UserBLL { Email = model.Email, Password = model.Password });

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
                return new OkObjectResult("User account created");
            }
        }

        [AllowAnonymous]
        [HttpPost("reg/org")]
        public async Task<IActionResult> RegisterOrganisation([FromBody]OrganisationRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _organisationService.RegisterOrganisation(new OrganisationBLL { Email = model.Email, Password = model.Password, Location = model.Location, Name = model.Name});
                      
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
                return new OkObjectResult("Organisation account created");
            }
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _accountService.ChangePassword(model.Name, model.OldPassword, model.NewPassword);
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