using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CustomAtributtes
{
    public class CheckHasNotNullParam<T> : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool hasNotNullParam = false;
            var parameter = context.ActionArguments.Single(kvp => kvp.Value is T).Value;

            if (parameter == null)
            {
                context.Result = new BadRequestObjectResult("Parameter cannot be null");
                return;
            }

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(parameter);

                if (value != null)
                {
                    hasNotNullParam = true;
                }
            }
            
            if (!hasNotNullParam)
            {
                context.Result = new BadRequestObjectResult("Parameter cannot be null");
            }
        }
    }
}