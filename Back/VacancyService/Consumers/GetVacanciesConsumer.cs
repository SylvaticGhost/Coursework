using AutoMapper;
using Contracts;
using Contracts.Events.VacancyEvent;
using GlobalModels.Vacancy;
using MassTransit;
using VacancyService.Repositories;

namespace VacancyService.Consumers;

public sealed class GetVacanciesConsumer(IVacancyRepo repo, IMapper mapper) : IConsumer<GetVacanciesEvent>
{
    public async Task Consume(ConsumeContext<GetVacanciesEvent> context)
    {
        IEnumerable<Guid> vacancyIds = context.Message.VacancyIds;
        
        IEnumerable<Vacancy> vacancies = await repo.GetVacancies(vacancyIds);
        
        IEnumerable<VacancyDto> vacancyDtos = mapper.Map<IEnumerable<VacancyDto>>(vacancies);
        
        var result = ServiceBusResultFactory.SuccessResult(vacancyDtos);
        
        await context.RespondAsync(result);
    }
}