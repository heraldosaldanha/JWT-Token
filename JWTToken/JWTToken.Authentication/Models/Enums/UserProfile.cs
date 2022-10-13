using System;
using System.Collections.Generic;
using System.Text;

namespace JWTToken.Authentication.Models.Enums
{
    public enum UserProfile
    {
        Administrator = 999,
        Manager = 2,
        Supervisor = 1,
        Restricted = 0
    }
}
