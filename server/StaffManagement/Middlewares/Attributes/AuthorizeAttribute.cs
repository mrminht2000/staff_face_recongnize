using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace StaffManagement.Middlewares.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var accountId = context.HttpContext.Items["Account"];

            if (accountId == null)
            {
                throw new UnauthorizedAccessException("Not log in");
            }
        }
    }
}
