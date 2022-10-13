using System;
using System.Collections.Generic;
using System.Text;

namespace JWTToken.Authentication.Models.Interfaces
{
    public interface IUserAuthenticateDto : IUserAuthenticate, IDto
    {
        string Password { get; set; }
    }
}
