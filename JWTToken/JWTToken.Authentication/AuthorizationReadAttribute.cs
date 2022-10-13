using JWTToken.Authentication.Models.Enums;
using JWTToken.Authentication.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace JWTToken.Authentication
{
    public class AuthorizationReadAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var claimsIdentity = actionContext.HttpContext.User.Identity as ClaimsIdentity;
            var accessLogged = claimsIdentity?.Claims.FirstOrDefault(x => x.Type == "AccessLogged")?.Value;
            var userProfileLogged = claimsIdentity?.Claims.FirstOrDefault(x => x.Type == "UserProfileLogged")?.Value;

            actionContext.HttpContext.Items["AccessLogged"] = accessLogged;
            actionContext.HttpContext.Items["UserProfileLogged"] = userProfileLogged;

            foreach (var value in actionContext.ActionArguments)
            {
                if (value.Value == null)
                    continue;
                var props = new List<PropertyInfo>(value.Value.GetType().GetProperties());

                if (props.FirstOrDefault(p => p.Name == "AccessLogged") == null)
                    continue;

                var body = ((IDto)value.Value);

                if (body == null)
                {
                    actionContext.Result = new BadRequestObjectResult("Invalid content");
                    return;
                }

                body.AccessLogged = accessLogged;
                body.UserProfileLogged = (UserProfile)int.Parse(userProfileLogged);
            }
        }
    }
}
