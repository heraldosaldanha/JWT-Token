using JWTToken.Authentication.Services;
using JWTToken.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWTToken.Repository.DependencyInjection
{
    public static class DependencyInjectionRepository
    {
        public static void Config(IServiceCollection services)
        {
            services.AddScoped<IUserServiceAuthenticate,UserRepository>();
        }
    }
}
