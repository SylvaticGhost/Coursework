using Contracts;
using VacancyService.Repositories;
using MassTransit;

namespace CompanySvc.Consumers;

public sealed class GetCompanyShortInfo : IConsumer<GetCompanyShortInfoEvent>
{
    private readonly ICompanyRepo _companyRepo;

    public GetCompanyShortInfo(ICompanyRepo companyRepo)
    {
        _companyRepo = companyRepo;
    }

    public async Task Consume(ConsumeContext<GetCompanyShortInfoEvent> context)
    {
        var c =await _companyRepo.GetCompanyShortInfoById(context.Message.CompanyId);
        
        await context.RespondAsync(c);
    }
}