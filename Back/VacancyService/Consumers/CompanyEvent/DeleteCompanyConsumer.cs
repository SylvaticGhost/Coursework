using Contracts;
using MassTransit;
using VacancyService.Repositories;

namespace VacancyService.Consumers;

public sealed class DeleteCompanyConsumer : IConsumer<DeleteCompanyEvent>
{
    private readonly IVacancyRepo _vacancyRepo;

    public DeleteCompanyConsumer(IVacancyRepo vacancyRepo)
    {
        _vacancyRepo = vacancyRepo;
    }
    
    public async Task Consume(ConsumeContext<DeleteCompanyEvent> context)
    {
        await _vacancyRepo.DeleteCompanyVacancies(context.Message.CompanyId);
    }
}