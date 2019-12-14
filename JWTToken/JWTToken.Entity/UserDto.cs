using JWTToken.Authentication.Models.Enums;
using JWTToken.Authentication.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWTToken.Entity
{
    public class UserDto : IUserAuthenticateDto
    {
        public string Password { get; set; }
        public string Access { get; set; }
        public UserProfile UserProfile { get; set; }
        public long EnterpriseId { get; set; }
        public TypeLogin TypeLogin { get; set; }
        public string RefreshToken { get; set; }
        public string AccessLogged { get; set; }
        public UserProfile UserProfileLogged { get; set; }
        public long EnterpriseIdLogged { get; set; }
    }
}
