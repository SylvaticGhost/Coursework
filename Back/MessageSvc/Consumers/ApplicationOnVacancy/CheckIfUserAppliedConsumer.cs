using Contracts;
using Contracts.Events.Messages;
using MassTransit;
using MessageSvc.Repositories.VacancyMessageBoxRepo;

namespace MessageSvc.Consumers.ApplicationOnVacancy;

public sealed class CheckIfUserAppliedConsumer(IVacancyMessageBoxRepo repo,ILogger<CheckIfUserAppliedConsumer> logger) : IConsumer<CheckIfUserAppliedEvent>
{
    public async Task Consume(ConsumeContext<CheckIfUserAppliedEvent> context)
    {
        Guid userId = context.Message.UserId;
        Guid vacancyId = context.Message.VacancyId;

        try
        {
            bool result = await repo.CheckIfUserApplied(vacancyId, userId);
            var response = ServiceBusResultFactory.SuccessResult(result);
            await context.RespondAsync(response);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while checking if user applied" + e.StackTrace);
            var response = ServiceBusResultFactory.FailResult<bool>("Error while checking if user applied");
            await context.RespondAsync(response);
        }
    }
}