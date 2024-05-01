using Microsoft.AspNetCore.Mvc.Filters;

namespace AccountService.ExceptionFilters;

public class LoggingExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger<LoggingExceptionFilter> _logger;
    
    public LoggingExceptionFilter(ILogger<LoggingExceptionFilter> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "An error occurred");
    }
}