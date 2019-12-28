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

        private readonly IAccountManagerService<UserBLL> _accountManagerService;

        public AccountManagerController(IAccountManagerService<UserBLL> accountManagerService)
        {
            _accountManagerService = accountManagerService;
        }

        [HttpGet]
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

        [HttpPut("edit")]
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

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteAccount([FromBody] DeleteUserViewModel model)
        {
            var result = await _accountManagerService.DeleteAccount(model.UserName);
            if (result.Succeeded)
            {
                return new OkObjectResult("Account deleted");
            }
            return NotFound("Invalid user name");
        }
    }
}