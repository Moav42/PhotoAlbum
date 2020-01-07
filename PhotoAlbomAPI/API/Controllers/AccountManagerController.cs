using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.Models.ViewModels;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Extensions;

namespace API.Controllers
{
    /// <summary>
    /// The controller representing the resource for managing the users and orgonisations accounts
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class AccountManagerController : ControllerBase
    {

        private readonly IAccountManagerService<UserBLL> _accountManagerService;
        private readonly IOrganisationService<OrganisationBLL> _organisationService;

        /// <summary>
        /// Configures the controller with the appropriate services using the dependency injection 
        /// </summary>
        /// <param name="organisationService"></param>
        /// <param name="accountService"></param>
        public AccountManagerController(IAccountManagerService<UserBLL> accountManagerService, IOrganisationService<OrganisationBLL> organisationService)
        {
            _accountManagerService = accountManagerService;
            _organisationService = organisationService;
        }

        /// <summary>
        /// Gets all application users accounts
        /// </summary>
        /// <returns>If result success returns users accounts, if it's not return NotFound</returns>
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAllUsers()
        {
            var users = await _accountManagerService.GetAllUsers();
            var userVM = new List<UserViewModel>();
            foreach (var item in users)
            {
                userVM.Add(new UserViewModel { Email = item.Email, UserId = item.UserId, UserName = item.UserName });
            }
            if(userVM.Count == 0)
            {
                return NotFound();
            }
            return Ok(userVM);
        }

        /// <summary>
        /// Gets all orgonsiations accounts
        /// </summary>
        /// <returns>If result success returns orgonsiations accounts, if it's not return NotFound</returns>
        [HttpGet("organisation")]
        public async Task<ActionResult<IEnumerable<OrganisationModel>>> GetAllOrganisation()
        {
            var orgs = await _organisationService.GetAllOrganisationAccountsAsync();
            var orgsModel = new List<OrganisationModel>();
            foreach (var item in orgs)
            {
                orgsModel.Add(new OrganisationModel { Id = item.Id, Name = item.Name, Location = item.Location });
            }

            if (orgsModel.Count == 0)
            {
                return NotFound();
            }

            return Ok(orgsModel);
        }

        /// <summary>
        /// Register a new user account with given role
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// If the provided model is not valid returns a BadRequest with the state of the model, 
        /// If the result is successful, returns the created model
        /// </returns>
        [HttpPost("create")]    
        public async Task<IActionResult> CreacteAccount([FromBody]AccountCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountManagerService.CreacteAccount(model.Email, model.Password, model.Role);

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
        /// Edits user account
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// If the provided model is not valid returns a BadRequest with the state of the model, 
        /// If the result is successful, returns the edits model
        /// </returns>
        [HttpPut("users")]
        public async Task<IActionResult> EditUserAccount([FromBody] UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountManagerService.EditUserAccount(model.UserId, model.UserName, model.Email);
                if (result.Succeeded)
                {
                    return Ok(model);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }
        /// <summary>
        /// Delets user account
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// If the provided model is not valid returns a BadRequest with the state of the model, 
        /// If the result is successful, returns NoContent, if user doesn't exist retunr NotFound
        /// </returns>
        [HttpDelete("users")]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteUserAccount([FromBody] DeleteUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountManagerService.DeleteAccount(model.UserName);
                if (result.Succeeded)
                {
                    return NoContent();
                }
                return NotFound("Invalid user name");
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Edits orgonistions account
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="model"></param>
        /// <returns>
        /// If the provided model is not valid returns a BadRequest with the state of the model, 
        /// If the result is successful, returns the edits model
        /// </returns>

        [HttpPut("organisation/{orgId}")]
        public async Task<IActionResult> EditOrganisationAccount(int orgId, OrganisationModel model)
        {
            if(orgId != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {            
                try
                {
                    await _organisationService.UpdateOrganisationAccountAsync(model.Transform());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _organisationService.GetOrganisationAccountAsync(model.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(model);
            }
            return BadRequest("Not a valid model");
        }

        /// <summary>
        /// Delets orgonisation account
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// If the provided model is not valid returns a BadRequest with the state of the model, 
        /// If the result is successful, returns NoContent, if user doesn't exist retunr NotFound
        /// </returns>
        [HttpDelete("organisation/{orgId}")]
        public async Task<ActionResult<OrganisationModel>> DeleteOrganisationAccount(int orgId)
        {
            if (ModelState.IsValid)
            {
                var model = await _organisationService.GetOrganisationAccountAsync(orgId);
                if (model == null)
                {
                    return NotFound();
                }

                await _organisationService.DeleteOrganisationAccountAsync(orgId);

                return NoContent();
            }
            return BadRequest(ModelState);
        }
    }
}