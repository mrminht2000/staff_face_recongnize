using Microsoft.AspNetCore.Mvc.Filters;
using System;
using static StaffManagement.Core.Core.Common.Enum.UserEnum;

namespace StaffManagement.API.Middlewares.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminOrOwnerAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var id = context.HttpContext?.Request.Headers["UserId"];
            var currentId = context.HttpContext.Items["Account"];

            if (!string.IsNullOrEmpty(id) && id.ToString().Trim() == currentId.ToString().Trim())
            {
                return;
            }
            var roleId = context.HttpContext.Items["AccountRole"];

            if (roleId == null || Convert.ToInt32(roleId) < (int)Role.Admin)
            {
                throw new UnauthorizedAccessException("User is not admin or owner");
            }
        }
    }

}
