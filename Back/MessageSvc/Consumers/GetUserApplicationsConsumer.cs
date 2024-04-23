using Contracts;
using Contracts.Events.ResponseOnVacancyEvents;
using GlobalModels.Messages;
using MassTransit;
using MessageSvc.Repositories.VacancyMessageBoxRepo;

namespace MessageSvc.Consumers;

public sealed class GetUserApplicationsConsumer(IVacancyMessageBoxRepo vacancyMessageBoxRepo) : IConsumer<GetUserApplicationsEvent>
{
    public async Task Consume(ConsumeContext<GetUserApplicationsEvent> context)
    {
        try
        {
            var applications = await vacancyMessageBoxRepo.GetUserApplications(context.Message.UserId);
            var result = ServiceBusResultFactory.SuccessResult(applications);
            await context.RespondAsync(result);
        }
        catch (Exception e)
        {
            var result = ServiceBusResultFactory.FailResult<IEnumerable<UserApplicationOnVacancy>>(e.Message);
            await context.RespondAsync(result);
        }
    }
}