using Microsoft.AspNetCore.Mvc.Filters;
using System;
using static StaffManagement.Core.Common.Enum.UserEnum;

namespace StaffManagement.Middlewares.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminRequireAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var roleId = context.HttpContext.Items["AccountRole"];

            if (roleId == null || Convert.ToInt32(roleId) < (int)Role.Admin)
            {
                throw new UnauthorizedAccessException("User is not admin");
            }
        }
    }

}
