using Contracts;
using Contracts.Events.VacancyEvent;
using GlobalModels.Messages;
using MassTransit;
using MessageSvc.Repositories.VacancyMessageBoxRepo;
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
// ReSharper disable once ClassNeverInstantiated.Global

namespace MessageSvc.Consumers.ApplicationOnVacancy;

public sealed class ApplicationForVacancyPostConsumer
    : IConsumer<ApplicationOnVacancyPostEvent>
{
    private readonly IVacancyMessageBoxRepo _vacancyMessageBoxRepo;
    private readonly IRequestClient<CheckIfVacancyExistEvent> _checkIfVacancyExistClient;
    private readonly IRequestClient<GetOwnerOfVacancyEvent> _getOwnerOfVacancyClient;
    private readonly ILogger<ApplicationForVacancyPostConsumer> _logger;
    
    public ApplicationForVacancyPostConsumer(
        IVacancyMessageBoxRepo vacancyMessageBoxRepo, 
        IRequestClient<CheckIfVacancyExistEvent> checkIfVacancyExistClient,
        IRequestClient<GetOwnerOfVacancyEvent> getOwnerOfVacancyClient,
        ILogger<ApplicationForVacancyPostConsumer> logger)
    {
        _vacancyMessageBoxRepo = vacancyMessageBoxRepo;
        _checkIfVacancyExistClient = checkIfVacancyExistClient;
        _getOwnerOfVacancyClient = getOwnerOfVacancyClient;
        _logger = logger;

        CheckDependencies();
    }
    
    
    public async Task Consume(ConsumeContext<ApplicationOnVacancyPostEvent> context)
    {
        string validationMessage = ValidateContext(context);
        if (!string.IsNullOrEmpty(validationMessage))
        {
            var result = ServiceBusResultFactory.FailResult<bool>(validationMessage);
            await context.RespondAsync(result);
            return;
        }
        
        bool vacancyHasBox =
            await _vacancyMessageBoxRepo.CheckIfVacancyHasApplicationBox(context.Message.ResponseOnVacancy.VacancyId);

        if (!vacancyHasBox)
        {
            CheckIfVacancyExistEvent checkIfVacancyExistEvent = new(context.Message.ResponseOnVacancy.VacancyId);
            var response = await _checkIfVacancyExistClient.GetResponse<IServiceBusResult<bool>>(checkIfVacancyExistEvent);
            
            if(response is null)
            {
                _logger.LogError("Response is null at ApplicationForVacancyPostConsumer.cs: CheckIfVacancyExistEvent");
                var result = ServiceBusResultFactory.FailResult<bool>("Trouble at service");
                await context.RespondAsync(result);
                return;
            }
            
            if (!response.Message.Result)
            {
                var result = ServiceBusResultFactory.FailResult<bool>("Vacancy does not exist");
                await context.RespondAsync(result);
                return;
            }

            try
            {
                GetOwnerOfVacancyEvent getOwnerOfVacancyEvent = new(context.Message.ResponseOnVacancy.VacancyId);
                var responseOwner = await _getOwnerOfVacancyClient.GetResponse<IServiceBusResult<Guid>>(getOwnerOfVacancyEvent);
                
                if(responseOwner is null)
                {
                    _logger.LogError("Response is null at ApplicationForVacancyPostConsumer.cs: GetOwnerOfVacancyEvent");
                    var result = ServiceBusResultFactory.FailResult<bool>("Trouble at service");
                    await context.RespondAsync(result);
                    return;
                }

                if (!responseOwner.Message.IsSuccess)
                {
                    FailedResponse(responseOwner.Message.ErrorMessage);
                    return;
                }
                
                Guid companyId = responseOwner.Message.Result;
                
                await _vacancyMessageBoxRepo.CreateMessageBox(context.Message.ResponseOnVacancy.VacancyId, companyId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + " at ApplicationForVacancyPostConsumer.cs: CheckIfVacancyExistEvent" + e.StackTrace);
                FailedResponse(e.Message);
                return;
            }
            
        }
        
        bool userHasApplication = 
            await _vacancyMessageBoxRepo.CheckIfUserHasApplication(context.Message.ResponseOnVacancy.VacancyId, context.Message.UserId);
        
        if(userHasApplication)
        {
            FailedResponse("User already has application on this vacancy");
            return;
        }
        
        UserApplicationOnVacancyToAddDto responseOnVacancy = context.Message.ResponseOnVacancy;

        var application = new UserApplicationOnVacancy(context.Message.UserId, responseOnVacancy.VacancyId, responseOnVacancy.ShortResume);

        try
        {
            await _vacancyMessageBoxRepo.AddApplication(application);
            IServiceBusResult<bool> result = ServiceBusResultFactory.SuccessResult(true);
            await context.RespondAsync(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message + " at ApplicationForVacancyPostConsumer.cs: AddApplication" + e.StackTrace);
            FailedResponse(e.Message);
        }

        return;


        async void FailedResponse(string message)
        {
            if (string.IsNullOrEmpty(message))
                message = "Failed to add application";

            ArgumentNullException.ThrowIfNull(context);

            Console.WriteLine("failed: " + message);
            var result = ServiceBusResultFactory.FailResult<bool>(message);
            await context.RespondAsync(result);
        }
    }
    
    private void CheckDependencies()
    {
        ArgumentNullException.ThrowIfNull(_vacancyMessageBoxRepo);

        ArgumentNullException.ThrowIfNull(_checkIfVacancyExistClient);

        ArgumentNullException.ThrowIfNull(_getOwnerOfVacancyClient);

        ArgumentNullException.ThrowIfNull(_logger);
    }
    
    
    private static string ValidateContext(ConsumeContext<ApplicationOnVacancyPostEvent> context)
    {
        if (context.Message.ResponseOnVacancy is null)
            return "ResponseOnVacancy is null";
        
        if (context.Message.ResponseOnVacancy.VacancyId == Guid.Empty)
            return "VacancyId is empty";
        
        if (context.Message.UserId == Guid.Empty)
            return "UserId is empty";
        
        if (context.Message.ResponseOnVacancy.ShortResume is null)
            return "ShortResume is null";

        return string.Empty;
    }
}