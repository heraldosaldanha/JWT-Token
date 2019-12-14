using JWTToken.Authentication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWTToken.Authentication.Models.Interfaces
{
    public interface IUserAuthenticate
    {
        string Access { get; set; }
        UserProfile UserProfile { get; set; }
        long EnterpriseId { get; set; }
        TypeLogin TypeLogin { get; set; }
        string RefreshToken { get; set; }
    }
}
