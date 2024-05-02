using Contracts;
using Contracts.Events.Messages;
using MassTransit;
using MessageSvc.Repositories.VacancyMessageBoxRepo;

namespace MessageSvc.Consumers.ApplicationOnVacancy;

public sealed class DeleteApplicationConsumer(IVacancyMessageBoxRepo repo) : IConsumer<DeleteUsersApplicationEvent>
{
    public async Task Consume(ConsumeContext<DeleteUsersApplicationEvent> context)
    {
        DeleteUsersApplicationEvent message = context.Message;

        IServiceBusResult<bool>? result = null;

        try
        {
            if (message.DeleteByApplicationId)
            {
                await repo.DeleteApplications(message.ApplicationId.ToArray());
                result = ServiceBusResultFactory.SuccessResult(true);
            }
            else
            {
                await repo.DeleteApplication(message.VacancyId, message.UserId);
                result = ServiceBusResultFactory.SuccessResult(true);
            }
            
        }
        catch (Exception e)
        {
            result = ServiceBusResultFactory.FailResult<bool>(e.Message);
        }
        finally
        {
            await context.RespondAsync(result!);
        }
        
    }
}