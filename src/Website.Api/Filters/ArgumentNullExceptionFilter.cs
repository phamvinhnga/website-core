using Website.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Website.Api.Filters
{
    internal class ArgumentNullExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is not ArgumentNullException) return;
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            context.Result = new JsonResult(new
            {
                message = context.Exception.GetBaseException().Message
            });

            base.OnException(context);
        }
    }
}
