using JWTToken.Authentication;
using JWTToken.Authentication.Models;
using JWTToken.Authentication.Models.Interfaces;
using JWTToken.Authentication.Services;
using JWTToken.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JWTToken.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("public")]
        public IActionResult PublicInfo()
        {
            try
            {
                return Ok("Informations Publics.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize("Bearer")]
        [AuthorizationRead]
        [HttpGet("private")]
        public IActionResult PrivateInfo([FromHeader]UserDto dto)
        {
            try
            {
                return Ok("Private Information.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

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
