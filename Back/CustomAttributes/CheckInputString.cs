using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace CustomAttributes
{
    public class CheckInputString : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Count == 1 && context.ActionArguments.First().Value is string)
            {
                string? parameter = context.ActionArguments.First().Value as string;

                if (!string.IsNullOrWhiteSpace(parameter)) return;
                    context.Result = new BadRequestObjectResult("Parameters cannot be null");
                
            }
            else
            {
                context.Result = new BadRequestObjectResult("Expected a single string parameter");
            }
        }
    }
}