﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.ViewModels;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountManagerController : ControllerBase
    {

        private readonly IAccountManagerService<UserBLL> _accountManagerService;
        private readonly IOrganisationService<OrganisationBLL> _organisationService;

        public AccountManagerController(IAccountManagerService<UserBLL> accountManagerService, IOrganisationService<OrganisationBLL> organisationService)
        {
            _accountManagerService = accountManagerService;
            _organisationService = organisationService;
        }

        [HttpGet("users")]
        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {
            var users = await _accountManagerService.GetAllUsers();
            var userVM = new List<UserViewModel>();
            foreach (var item in users)
            {
                userVM.Add(new UserViewModel { Email = item.Email, UserId = item.UserId, UserName = item.UserName });
            }
            return userVM;
        }

        [HttpGet("organisation")]
        public async Task<IEnumerable<OrganisationModel>> GetAllOrganisation()
        {
            var orgs = await _organisationService.GetAllAsync();
            var orgsModel = new List<OrganisationModel>();
            foreach (var item in orgs)
            {
                orgsModel.Add(new OrganisationModel { Id = item.Id, Name = item.Name, Location = item.Location });
            }
            return orgsModel;
        }

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
                return new OkObjectResult("Account created");
            }
        }

        [HttpPut("users/edit")]
        public async Task<IActionResult> EditUserAccount([FromBody] EditUserNameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountManagerService.EditUserAccount(model.OldName, model.NewName);
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
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("users/delete")]
        public async Task<ActionResult> DeleteUserAccount([FromBody] DeleteUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountManagerService.DeleteAccount(model.UserName);
                if (result.Succeeded)
                {
                    return new OkObjectResult("Account deleted");
                }
                return NotFound("Invalid user name");
            }
            return BadRequest(ModelState);
        }

        [HttpPut("organisation/edit")]
        public async Task<IActionResult> EditOrganisationAccount([FromBody] EditOrganisationViewModel model)
        {
            if (ModelState.IsValid)
            {            
                try
                {
                    await _organisationService.UpdateAsync(new OrganisationBLL { Id = model.Id, Name = model.OrgName, Location = model.Loacation });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _organisationService.GetAsync(model.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return new OkObjectResult(model);
            }
            return BadRequest("Not a valid model");
        }

        [HttpDelete("organisation/delete")]
        public async Task<ActionResult> DeleteOrganisationAccount([FromBody]DeleteOrganisationViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _organisationService.DeleteAsync(model.OrgId);
                var result = await _accountManagerService.DeleteAccount(model.UserName);
                if (result.Succeeded)
                {
                    return new OkObjectResult("Account deleted");
                }
                return NotFound("Invalid user name");
            }
            return BadRequest(ModelState);
        }
    }
}