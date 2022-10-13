using JWTToken.Entity;
using System.Collections.Generic;
using JWTToken.Authentication.Models.Enums;

namespace JWTToken.Repository.Fakes
{
    public class UserFakes
    {
        public IList<User> Users { get; }

        public UserFakes()
        {
            Users = new List<User>();
            Users.Add(new User()
            {
                Access = "User Manager",
                Password = "123",
                UserProfile = UserProfile.Manager
            });

            Users.Add(new User()
            {
                Access = "User Restricted",
                Password = "123",
                UserProfile = UserProfile.Restricted
            });
        }
    }
}
