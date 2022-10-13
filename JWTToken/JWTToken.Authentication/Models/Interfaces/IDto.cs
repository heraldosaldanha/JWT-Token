using JWTToken.Authentication.Models.Enums;

namespace JWTToken.Authentication.Models.Interfaces
{
    public interface IDto
    {
        string AccessLogged { get; set; }
        UserProfile UserProfileLogged { get; set; }
    }
}
