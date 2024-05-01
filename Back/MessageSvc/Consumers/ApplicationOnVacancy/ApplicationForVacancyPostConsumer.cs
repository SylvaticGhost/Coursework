using Contracts;
using Contracts.Events.VacancyEvent;
using GlobalModels.Messages;
using MassTransit;
using MessageSvc.Repositories.VacancyMessageBoxRepo;

namespace MessageSvc.Consumers.ApplicationOnVacancy;

public sealed class ApplicationForVacancyPostConsumer
    (IVacancyMessageBoxRepo vacancyMessageBoxRepo, IRequestClient<CheckIfVacancyExistEvent> checkIfVacancyExistClient,
        IRequestClient<GetOwnerOfVacancyEvent> getOwnerOfVacancyClient)
    : IConsumer<ApplicationOnVacancyPostEvent>
{
    public async Task Consume(ConsumeContext<ApplicationOnVacancyPostEvent> context)
    {
        bool vacancyHasBox =
            await vacancyMessageBoxRepo.CheckIfVacancyHasApplicationBox(context.Message.ResponseOnVacancy.VacancyId);

        if (!vacancyHasBox)
        {
            CheckIfVacancyExistEvent checkIfVacancyExistEvent = new(context.Message.ResponseOnVacancy.VacancyId);
            var response = await checkIfVacancyExistClient.GetResponse<IServiceBusResult<bool>>(checkIfVacancyExistEvent);
            
            if (!response.Message.Result)
            {
                var result = ServiceBusResultFactory.FailResult<bool>("Vacancy does not exist");
                await context.RespondAsync(result);
                return;
            }

            try
            {
                GetOwnerOfVacancyEvent getOwnerOfVacancyEvent = new(context.Message.ResponseOnVacancy.VacancyId);
                var responseOwner = await getOwnerOfVacancyClient.GetResponse<IServiceBusResult<Guid>>(getOwnerOfVacancyEvent);

                if (!responseOwner.Message.IsSuccess)
                {
                    FailedResponse(responseOwner.Message.ErrorMessage);
                    return;
                }
                
                Guid companyId = responseOwner.Message.Result;
                
                await vacancyMessageBoxRepo.CreateMessageBox(context.Message.ResponseOnVacancy.VacancyId, companyId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                FailedResponse(e.Message);
                return;
            }
            
        }
        
        bool userHasApplication = 
            await vacancyMessageBoxRepo.CheckIfUserHasApplication(context.Message.ResponseOnVacancy.VacancyId, context.Message.UserId);
        
        if(userHasApplication)
        {
            FailedResponse("User already has application on this vacancy");
            return;
        }
        
        UserApplicationOnVacancyToAddDto responseOnVacancy = context.Message.ResponseOnVacancy;

        var application = new UserApplicationOnVacancy()
        {
            UserId = context.Message.UserId,
            VacancyId = responseOnVacancy.VacancyId,
            ShortResume = responseOnVacancy.ShortResume
        };

        try
        {
            await vacancyMessageBoxRepo.AddApplication(application);
            IServiceBusResult<bool> result = ServiceBusResultFactory.SuccessResult(true);
            await context.RespondAsync(result);
        }
        catch (Exception e)
        {
            FailedResponse(e.Message);
        }

        return;


        async void FailedResponse(string message)
        {
            Console.WriteLine("failed: " + message);
            var result = ServiceBusResultFactory.FailResult<bool>(message);
            await context.RespondAsync(result);
        }
    }
}