using Website.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Website.Api.Filters
{
    internal class BadRequestExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is not BadRequestException exception) return;

            context.HttpContext.Response.StatusCode = exception is { Code: 0 } ? StatusCodes.Status400BadRequest : exception.Code;
            context.Result = new JsonResult(new
            {
                message = exception.GetBaseException().Message
            });
            base.OnException(context);
        }
    }
}
