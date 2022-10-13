using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JWTToken.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class PublicController : ControllerBase
    {

        [AllowAnonymous]
        [HttpGet]
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
    }
}
