using System.Threading.Tasks;
using API.Models.ViewModels;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// The controller representing the resource for managing the user account
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Policy = "AllUsers")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService<UserBLL> _accountService;
        private readonly IOrganisationService<OrganisationBLL> _organisationService;

        /// <summary>
        /// Configures the controller with the appropriate services using the dependency injection 
        /// </summary>
        /// <param name="organisationService"></param>
        /// <param name="accountService"></param>
        public AccountController( IOrganisationService<OrganisationBLL> organisationService, IAccountService<UserBLL> accountService )
        {      
            _organisationService = organisationService;
            _accountService = accountService;
        }

        /// <summary>
        /// Register a new application user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// If the provided model is not valid returns a BadRequest with the state of the model, 
        /// If the result is successful, returns the created model
        /// </returns>
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
                return Ok(model);
            }
        }
        /// <summary>
        /// Register a new application user with orgonisation role
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// If the provided model is not valid returns a BadRequest with the state of the model, 
        /// If the result is successful, returns the created model
        /// </returns>

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
                return Ok(model);
            }
        }

        /// <summary>
        /// Changes password of given user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// If the provided model is not valid returns a BadRequest with the state of the model, 
        /// If the result is successful, returns OK respons
        /// </returns>
        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _accountService.ChangePassword(model.Name, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return  Ok("Password changed");
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