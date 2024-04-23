using Contracts;
using Contracts.Events.VacancyEvent;
using MassTransit;
using VacancyService.Repositories;

namespace VacancyService.Consumers;

public sealed class GetOwnerOfVacancyConsumer(IVacancyRepo repo) : IConsumer<GetOwnerOfVacancyEvent>
{
    public async Task Consume(ConsumeContext<GetOwnerOfVacancyEvent> context)
    {
        Guid vacancyId = context.Message.VacancyId;
        var ownerId = await repo.GetOwnerOfVacancy(vacancyId);

        var result = ownerId == Guid.Empty ?
            ServiceBusResultFactory.FailResult<Guid>("Vacancy not found") 
            : ServiceBusResultFactory.SuccessResult(ownerId);
        
        await context.RespondAsync(result);
    }
}