using JWTToken.Authentication.Models.Enums;
using JWTToken.Authentication.Models.Interfaces;

namespace JWTToken.Entity
{
    public class User : IUserAuthenticate
    {
        public string Access { get; set; }
        public string Password { get; set; }
        public UserProfile UserProfile { get; set; }
        public TypeLogin TypeLogin { get; set; }
        public string RefreshToken { get; set; }
    }
}
