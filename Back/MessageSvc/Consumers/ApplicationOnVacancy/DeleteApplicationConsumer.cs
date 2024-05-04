using Contracts;
using Contracts.Events.Messages;
using MassTransit;
using MessageSvc.Repositories.VacancyMessageBoxRepo;

namespace MessageSvc.Consumers.ApplicationOnVacancy;

public sealed class DeleteApplicationConsumer(IVacancyMessageBoxRepo repo, ILogger<DeleteApplicationConsumer> logger) : 
    IConsumer<DeleteUsersApplicationEvent>,
    IConsumer<DeleteUserApplicationByVacancyEvent>
{
    public async Task Consume(ConsumeContext<DeleteUsersApplicationEvent> context)
    {
        DeleteUsersApplicationEvent message = context.Message;

        IServiceBusResult<bool>? result = null;

        try
        {
            await repo.DeleteApplications(message.ApplicationId);
            result = ServiceBusResultFactory.SuccessResult(true);
        }
        catch (Exception e)
        {
            result = ServiceBusResultFactory.FailResult<bool>(e.Message);
            logger.LogError(e.Message, e.StackTrace);
        }
        finally
        {
            await context.RespondAsync(result!);
        }
    }
    
    
    public async Task Consume(ConsumeContext<DeleteUserApplicationByVacancyEvent> context)
    {
        DeleteUserApplicationByVacancyEvent message = context.Message;

        IServiceBusResult<bool>? result = null;

        try
        {
            await repo.DeleteApplication(message.VacancyId, message.UserId);
            result = ServiceBusResultFactory.SuccessResult(true);
        }
        catch (Exception e)
        {
            result = ServiceBusResultFactory.FailResult<bool>(e.Message);
            logger.LogError(e.Message, e.StackTrace);
        }
        finally
        {
            await context.RespondAsync(result!);
        }
    }
    
}