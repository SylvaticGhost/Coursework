using Contracts;
using Contracts.Events.VacancyEvent;
using MassTransit;
using VacancyService.Repositories;

namespace VacancyService.Consumers;

public sealed class CheckIfVacancyExistConsumer(IVacancyRepo repo) : IConsumer<CheckIfVacancyExistEvent>
{
    public async Task Consume(ConsumeContext<CheckIfVacancyExistEvent> context)
    {
        bool result = await repo.CheckIfVacancyExists(context.Message.VacancyId);
        
        IServiceBusResult<bool> response = ServiceBusResultFactory.SuccessResult(result);
        
        await context.RespondAsync(response);
    }
}