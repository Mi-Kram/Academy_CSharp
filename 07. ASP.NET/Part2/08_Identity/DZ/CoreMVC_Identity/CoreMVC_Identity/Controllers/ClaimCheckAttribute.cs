using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreMVC_Identity.Controllers
{
    [NonController]
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ClaimCheckAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
                return;

            var isAuthorized = user.Identity is ClaimsIdentity      // у пользователя есть Claims
                && ((ClaimsIdentity)user.Identity).HasClaim(t => t.Type == ClaimType && t.Value == ClaimValue);

            if(!isAuthorized)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
