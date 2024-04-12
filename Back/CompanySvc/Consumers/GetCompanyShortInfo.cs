using CompanySvc.Repositories;
using Contracts;
using MassTransit;
using VacancyService.Models;

namespace CompanySvc.Consumers;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class GetCompanyShortInfo(ICompanyRepo companyRepo) : IConsumer<GetCompanyShortInfoEvent>
{
    public async Task Consume(ConsumeContext<GetCompanyShortInfoEvent> context)
    {
        CompanyShortInfo? c = await companyRepo.GetCompanyShortInfoById(context.Message.CompanyId);
        
        await context.RespondAsync(c!);
    }
}