using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using CompanySvc.Helpers;
using GlobalHelpers;
using GlobalModels.Messages.CompanyResponse;

namespace CompanySvc.MiddleWare.CompanyMessageController;

public class ValidateAnswerOnApplicationAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var answerOnApplicationDto = context.ActionArguments.Values
            .OfType<AnswerOnApplicationToAddDto>()
            .FirstOrDefault();

        if (answerOnApplicationDto == null)
        {
            context.Result = new BadRequestObjectResult("Missing AnswerOnApplicationToAddDto object");
            return;
        }
        
        // Perform your validation here
        var validationResults = LocalValidator.ValidateAnswerOnApplication(answerOnApplicationDto);

        if (!validationResults.IsValid)
        {
            context.Result = new BadRequestObjectResult(validationResults.ToString());
        }
    }
}