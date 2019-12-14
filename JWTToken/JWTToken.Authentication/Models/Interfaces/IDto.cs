using JWTToken.Authentication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWTToken.Authentication.Models.Interfaces
{
    public interface IDto
    {
        string AccessLogged { get; set; }
        UserProfile UserProfileLogged { get; set; }
        long EnterpriseIdLogged { get; set; }
    }
}
