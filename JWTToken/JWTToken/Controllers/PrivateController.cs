using JWTToken.Authentication;
using JWTToken.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JWTToken.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class PrivateController : ControllerBase
    {
        [Authorize("Bearer")]
        [AuthorizationRead]
        [HttpGet]
        public IActionResult PrivateInfo([FromHeader] UserDto dtoHeader)
        {
            try
            {

                return Ok(dtoHeader);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
