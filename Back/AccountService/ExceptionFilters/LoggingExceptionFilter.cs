using Microsoft.AspNetCore.Mvc.Filters;

namespace AccountService.ExceptionFilters
{
    public class LoggingExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService<ILogger<LoggingExceptionFilter>>();
            logger!.LogError(context.Exception, "An error occurred");
        }
    }
}