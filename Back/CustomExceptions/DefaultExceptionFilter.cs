using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;


namespace CustomExceptions;

public class DefaultExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        if (exception is ICustomException customException)
        {
            context.HttpContext.Response.StatusCode = customException.StatusCode;
            context.Result = new JsonResult(new
                {
                    message = exception.Message,
                    statusCode = customException.StatusCode
                }
            );
        }
        else
        {
            context.HttpContext.Response.StatusCode = 500;
            context.Result = new JsonResult(new { message = "Internal server error" });
        }

        base.OnException(context);
    }
}