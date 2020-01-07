using System.Threading.Tasks;
using API.Models.ViewModels;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller responsible for user authorization
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AllUsers")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService<UserBLL> _authorizationService;

        public AuthorizationController(IAuthorizationService<UserBLL> authorizationService)
        {
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Authorizes the user by providing him JWT with claims based on the given credentials
        /// </summary>
        /// <param name="credentials">Login and Password of the user</param>
        /// <returns>
        /// If provided credentials are valid return JWT, else return  BadRequest.
        /// </returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jwt = await _authorizationService.GetJWT(credentials.Username, credentials.Password);

            if (jwt == null)
            {
                return BadRequest("Wrong login or password");
            }

            return Ok(jwt);
        }     
    }
}