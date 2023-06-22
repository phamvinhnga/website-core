using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Website.Shared.Exceptions;

namespace Website.Api.Filters
{
    public class UnauthorizedExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is not UnauthorizedException exception) return;

            context.HttpContext.Response.StatusCode = exception is { Code: 0 } ? StatusCodes.Status403Forbidden : exception.Code;

            context.Result = new JsonResult(new
            {
                message = exception.GetBaseException().Message
            });
            base.OnException(context);
        }
    }
}
