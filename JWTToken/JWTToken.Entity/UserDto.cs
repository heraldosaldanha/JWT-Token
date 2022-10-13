using JWTToken.Authentication.Models.Enums;
using JWTToken.Authentication.Models.Interfaces;

namespace JWTToken.Entity
{
    public class UserDto : IUserAuthenticateDto
    {
        public string Password { get; set; }
        public string Access { get; set; }
        public UserProfile UserProfile { get; set; }
        public TypeLogin TypeLogin { get; set; }
        public string RefreshToken { get; set; }
        public string AccessLogged { get; set; }
        public UserProfile UserProfileLogged { get; set; }
    }
}
