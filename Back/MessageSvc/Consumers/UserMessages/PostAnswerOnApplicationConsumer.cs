using Contracts;
using Contracts.Events.Messages;
using GlobalModels.Messages.CompanyResponse;
using MassTransit;
using MessageSvc.Repositories.UserMessageBoxRepo;
using MessageSvc.Repositories.VacancyMessageBoxRepo;

namespace MessageSvc.Consumers.UserMessages;

public sealed class PostAnswerOnApplicationConsumer
    (IUserMessageBoxRepo messageBoxRepo, IVacancyMessageBoxRepo vacancyMessageBoxRepo, ILogger<PostAnswerOnApplicationConsumer> logger) 
    : IConsumer<PostAnswerOnApplicationEvent>
{
    public async Task Consume(ConsumeContext<PostAnswerOnApplicationEvent> context)
    {
        PostAnswerOnApplicationEvent message = context.Message;

        try
        {
            Guid vacancyId = context.Message.Answer.VacancyId;
            Guid applicationId = context.Message.Answer.UserApplicationId;
            
            Guid userId = await vacancyMessageBoxRepo.GetUserIdFromApplication
                (applicationId: applicationId, vacancyId: vacancyId);
        
            if(userId == Guid.Empty)
                throw new Exception("User not found");
            
            AnswerOnApplication answer = new(
                context.Message.Answer, 
                context.Message.CompanyId, 
                context.Message.CompanyName, 
                userId);
            
            await messageBoxRepo.AddAnswerOnApplication(answer);

            IServiceBusResult<bool> response = ServiceBusResultFactory.SuccessResult(true);
            
            await context.RespondAsync(response);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while adding answer on application");
            IServiceBusResult<bool> response = ServiceBusResultFactory.FailResult<bool>(e.Message);
            await context.RespondAsync(response);
        }
    }
}