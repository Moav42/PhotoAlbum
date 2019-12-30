using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models.ViewModels;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService<UserBLL> _authorizationService;

        public AuthorizationController(IAuthorizationService<UserBLL> authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var jwt = await _authorizationService.GetJWT(credentials.UserName, credentials.Password);

            if(jwt == null)
            {
                return BadRequest("Wrong login or password");
            }

            return new OkObjectResult(jwt);
        }

     
    }
}