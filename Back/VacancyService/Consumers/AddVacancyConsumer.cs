using Contracts;
using MassTransit;
using VacancyService.Repositories;

namespace VacancyService.Consumers;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class AddVacancyConsumer(IVacancyRepo vacancyRepo) : IConsumer<AddVacancyEvent>
{
    public async Task Consume(ConsumeContext<AddVacancyEvent> context)
    {
        try
        {
            Guid vacancyId = await vacancyRepo.AddVacancy(context.Message.Vacancy, context.Message.CompanyShortInfo, context.Message.Time);
            var result = ServiceBusResultFactory.SuccessResult(vacancyId);
            await context.RespondAsync(result);
        }
        catch (Exception e)
        {
            var result = ServiceBusResultFactory.FailResult<Guid>(e.Message);
            await context.RespondAsync(result);
        }
    }
}