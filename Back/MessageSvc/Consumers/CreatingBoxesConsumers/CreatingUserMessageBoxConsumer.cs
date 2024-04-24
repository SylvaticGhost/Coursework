using Contracts;
using Contracts.Events.Messages.CreatingBoxEvents;
using MassTransit;
using MessageSvc.Models;
using MessageSvc.Repositories.UserMessageBoxRepo;

namespace MessageSvc.Consumers;

public sealed class CreatingUserMessageBoxConsumer(IUserMessageBoxRepo messageBoxRepo) : IConsumer<CreateUserMessageBoxEvent>
{
    public async Task Consume(ConsumeContext<CreateUserMessageBoxEvent> context)
    {
        if(await messageBoxRepo.UserMessageBoxExists(context.Message.UserId))
        {
            var result = ServiceBusResultFactory.FailResult<bool>("User message box already exists");
            await context.RespondAsync(result);
            return;
        }

        try
        {
            await messageBoxRepo.CreateUserMessageBox(context.Message.UserId);
            var result = ServiceBusResultFactory.SuccessResult(true);
            await context.RespondAsync(result);
        }
        catch (Exception e)
        {
            var result = ServiceBusResultFactory.FailResult<bool>(e.Message);
            await context.RespondAsync(result);
        }
    }
}