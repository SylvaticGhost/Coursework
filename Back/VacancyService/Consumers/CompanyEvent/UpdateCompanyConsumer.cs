using Contracts;
using VacancyService.Repositories;
using MassTransit;
using VacancyService.Models;

namespace VacancyService.Consumers;

public sealed class UpdateCompanyConsumer : IConsumer<UpdateCompanyEvent>
{
    private readonly IVacancyRepo _vacancyRepo;

    public UpdateCompanyConsumer(IVacancyRepo vacancyRepo)
    {
        _vacancyRepo = vacancyRepo;
    }
    
    public async Task Consume(ConsumeContext<UpdateCompanyEvent> context)
    {
        CompanyShortInfo companyInfo = new CompanyShortInfo()
        {
            CompanyId = context.Message.CompanyId,
            Address = context.Message.Address,
            CompanyEmail = context.Message.Email,
            Name = context.Message.Name,
            PhoneNumber = context.Message.PhoneNumber
        };

        await _vacancyRepo.UpdateCompanyInfoInVacancies(companyInfo);
    }
}