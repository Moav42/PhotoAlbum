using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<DAL.Entities.User> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<DAL.Entities.User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }



        [HttpPost]
        public async Task<IActionResult> PostRole([FromBody] RoleViewModel role)
        {
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(role.Name));
            if (result.Succeeded)
            {
                return new OkObjectResult("Role " + role.Name + " created");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return new BadRequestObjectResult(result.Errors);
            }

        }
    }
}