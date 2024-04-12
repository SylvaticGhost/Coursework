using Contracts;
using GlobalModels.Vacancy;
using MassTransit;
using VacancyService.Models;
using VacancyService.SearchContext;
// ReSharper disable ClassNeverInstantiated.Global

namespace VacancyService.Consumers;

public sealed class GetCompanyVacanciesConsumer(ISearchVacancyContext searchContext) : IConsumer<GetCompanyVacanciesEvent>
{
    public async Task Consume(ConsumeContext<GetCompanyVacanciesEvent> context)
    {
        IServiceBusResult<IEnumerable<Vacancy>> result;
        try
        {
            SearchVacancyParams searchVacancyParams = new()
            {
                CompanyId = context.Message.CompanyId
            };
        
            IEnumerable<Vacancy> vacancies = searchContext.SearchVacancy(searchVacancyParams);

            result = new ServiceBusResult<IEnumerable<Vacancy>>()
            {
                Result = vacancies,
                IsSuccess = true
            };
        }
        catch(Exception ex)
        {
            result = new ServiceBusResult<IEnumerable<Vacancy>>()
            {
                ErrorMessage = ex.Message,
                IsSuccess = false
            };
        }

        await context.RespondAsync(result);
    }
}