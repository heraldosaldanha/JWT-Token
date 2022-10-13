using JWTToken.Authentication.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWTToken.Authentication.Services
{
    public interface IUserServiceAuthenticate
    {
        IUserAuthenticate GetForLogin(IUserAuthenticateDto userDto);
        IUserAuthenticate GetByAcess(string access);
    }
}
