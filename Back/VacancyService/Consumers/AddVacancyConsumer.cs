using Contracts;
using MassTransit;
using VacancyService.Repositories;

namespace VacancyService.Consumers;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class AddVacancyConsumer(IVacancyRepo vacancyRepo) : IConsumer<AddVacancyEvent>
{
    public async Task Consume(ConsumeContext<AddVacancyEvent> context)
    {
        await vacancyRepo.AddVacancy(context.Message.Vacancy, context.Message.CompanyShortInfo, context.Message.Time);
    }
}