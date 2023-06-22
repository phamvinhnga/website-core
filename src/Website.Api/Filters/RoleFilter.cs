using Website.Shared.Common;
using Website.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Website.Api.Filters
{
    public class AdminRoleFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Claims.IsAdmin()) return;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.Result = new JsonResult(new { message = Message.NoPermission });
        }
    }

    public class StaffRoleFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Claims.IsStaff()) return;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.Result = new JsonResult(new { message = Message.NoPermission });
        }
    }
}
