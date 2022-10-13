using JWTToken.Authentication;
using JWTToken.Authentication.Models;
using JWTToken.Authentication.Services;
using JWTToken.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JWTToken.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(
            [FromBody]UserDto userDto,
            [FromServices]AuthenticateManager authenticateManager,
            [FromServices]IUserServiceAuthenticate userService)
        {
            try
            {
                ObjectToken objectToken = authenticateManager.Authenticate(userDto, userService);
                if (objectToken.authenticated)
                    return Ok(objectToken);
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
