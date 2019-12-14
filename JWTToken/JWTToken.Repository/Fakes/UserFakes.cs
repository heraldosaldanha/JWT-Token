using JWTToken.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using JWTToken.Authentication.Models.Enums;
using JWTToken.Authentication.Models.Interfaces;

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
                EnterpriseId = 1,
                UserProfile = UserProfile.Manager
            });

            Users.Add(new User()
            {
                Access = "User Restricted",
                Password = "123",
                EnterpriseId = 1,
                UserProfile = UserProfile.Restricted
            });
        }
    }
}
