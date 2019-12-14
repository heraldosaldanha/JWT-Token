using JWTToken.Authentication.Models.Interfaces;
using JWTToken.Authentication.Services;
using JWTToken.Repository.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JWTToken.Repository.Repositories
{
    public class UserRepository : IUserServiceAuthenticate
    {
        private readonly UserFakes _userFake;

        public UserRepository()
        {
            _userFake = new UserFakes();
        }
        public IUserAuthenticate GetByAcess(string access)
        {
            return _userFake.Users.FirstOrDefault(x => x.Access == access);
        }

        public IUserAuthenticate GetForLogin(IUserAuthenticateDto userDto)
        {
            return _userFake.Users.FirstOrDefault(x => x.Access == userDto.Access && x.Password == userDto.Password);            
        }
    }
}
