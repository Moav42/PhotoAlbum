using System.Threading.Tasks;
using API.Models.ViewModels;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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