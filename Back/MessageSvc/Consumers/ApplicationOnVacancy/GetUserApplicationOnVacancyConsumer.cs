using Contracts;
using Contracts.Events.Messages;
using GlobalModels.Messages;
using MassTransit;
using MessageSvc.Repositories.VacancyMessageBoxRepo;

namespace MessageSvc.Consumers.ApplicationOnVacancy;

public sealed class GetUserApplicationOnVacancyConsumer(IVacancyMessageBoxRepo repo,
    ILogger<GetUserApplicationOnVacancyConsumer> logger) 
    : IConsumer<GetUserApplicationOnVacancyEvent>
{
    public async Task Consume(ConsumeContext<GetUserApplicationOnVacancyEvent> context)
    {
        Guid userId = context.Message.UserId;
        Guid vacancyId = context.Message.VacancyId;
        
        try
        {
            UserApplicationOnVacancy? application = await repo.GetUserApplicationOnVacancy(vacancyId, userId);
            var result = ServiceBusResultFactory.SuccessResult(application);
            
            await context.RespondAsync(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while getting user application on vacancy");
            var result = ServiceBusResultFactory.FailResult<UserApplicationOnVacancy>(e.Message);
            await context.RespondAsync(result);
        }
        
    }
}