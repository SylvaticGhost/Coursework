using Contracts;
using Contracts.Events.ResponseOnVacancyEvents;
using GlobalModels.Messages;
using MassTransit;
using MessageSvc.Repositories.VacancyMessageBoxRepo;

namespace MessageSvc.Consumers.ApplicationOnVacancy;

public sealed class GetUserApplicationsConsumer(IVacancyMessageBoxRepo vacancyMessageBoxRepo): IConsumer<GetUserApplicationsEvent>
{
    public async Task Consume(ConsumeContext<GetUserApplicationsEvent> context)
    {
        Guid userId = context.Message.UserId;
        
        IEnumerable<UserApplicationOnVacancy> userApplications = await vacancyMessageBoxRepo.GetUserApplications(userId);
        
        IServiceBusResult<IEnumerable<UserApplicationOnVacancy>> result = ServiceBusResultFactory.SuccessResult(userApplications)!;
        
        await context.RespondAsync(result);
    }
}