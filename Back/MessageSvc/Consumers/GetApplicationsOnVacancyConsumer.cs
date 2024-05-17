using Contracts;
using Contracts.Events.ResponseOnVacancyEvents;
using GlobalModels.Messages;
using MassTransit;
using MessageSvc.Repositories.VacancyMessageBoxRepo;

namespace MessageSvc.Consumers;

public sealed class GetApplicationsOnVacancyConsumer(IVacancyMessageBoxRepo repo, 
    ILogger<GetApplicationsOnVacancyConsumer> logger)
    : IConsumer<GetVacancyApplicationEvent>
{
    public async Task Consume(ConsumeContext<GetVacancyApplicationEvent> context)
    {
        Guid vacancyId = context.Message.VacancyId;

        try
        {
            IEnumerable<UserApplicationOnVacancy> applications = await repo.GetApplicationsOnVacancy(vacancyId);
        
            var result = ServiceBusResultFactory.SuccessResult(applications);
            await context.RespondAsync(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while getting applications on vacancy");
            var result = ServiceBusResultFactory.FailResult<IEnumerable<UserApplicationOnVacancy>>(e.Message);
            await context.RespondAsync(result);
        }
        
    }
}