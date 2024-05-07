using Contracts;
using Contracts.Events.Messages;
using GlobalModels.Messages.CompanyResponse;
using MassTransit;
using MessageSvc.Repositories.UserMessageBoxRepo;
using Guid = System.Guid;

namespace MessageSvc.Consumers.UserMessages;

public sealed class UserMessagesConsumer(IUserMessageBoxRepo repo, ILogger<UserMessagesConsumer> logger) 
    : IConsumer<GetAnswerForUserEvent>,
        IConsumer<DeleteAnswerEvent>
{
    public async Task Consume(ConsumeContext<GetAnswerForUserEvent> context)
    {
        Guid userId = context.Message.UserId;

        try
        {
            var answers = await repo.GetAnswersForUser(userId);

            IServiceBusResult<IEnumerable<AnswerOnApplication>?> response =
                ServiceBusResultFactory.SuccessResult(answers);

            await context.RespondAsync(response);
        }
        catch (Exception ex)
        {
            IServiceBusResult<IEnumerable<AnswerOnApplication>?> response = 
                ServiceBusResultFactory.FailResult<IEnumerable<AnswerOnApplication>?>(ex.Message);
            
            logger.LogError(ex.Message + ex.Source + ex.StackTrace);
            await context.RespondAsync(response);
        }
    }


    public async Task Consume(ConsumeContext<DeleteAnswerEvent> context)
    {
        Guid userId = context.Message.UserId;
        Guid applicationId = context.Message.ApplicationId;

        try
        {
            await repo.DeleteAnswer(userId, applicationId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message + ex.Source + ex.StackTrace);
        }
    }
}