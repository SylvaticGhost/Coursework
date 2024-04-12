// ReSharper disable ClassNeverInstantiated.Global
using Contracts;
using CustomExceptions._400s;
using MassTransit;
using VacancyService.Repositories;

namespace VacancyService.Consumers;

public sealed class DeleteVacancyConsumer(IVacancyRepo vacancyRepo) : IConsumer<DeleteVacancyEvent>
{
    public async Task Consume(ConsumeContext<DeleteVacancyEvent> context)
    {
        Console.WriteLine("DeleteVacancyConsumer: DeleteVacancyEvent received");
        try
        {
            bool companyCanDeleteVacancy = 
                await vacancyRepo.CheckIfCompanyOwnVacancy(context.Message.CompanyId, context.Message.VacancyId);
            
            if(!companyCanDeleteVacancy)
                throw new ForbiddenException("Company does not own this vacancy");
        
            await vacancyRepo.DeleteVacancy(context.Message.VacancyId);
            
            await context.RespondAsync<IServiceBusResult<bool>>(new ServiceBusResult<bool>
            {
                IsSuccess = true,
                Result = true
            });
        }
        catch (Exception e)
        {
            await context.RespondAsync<IServiceBusResult<bool>>(new ServiceBusResult<bool>
            {
                Result = false,
                IsSuccess = false,
                ErrorMessage = e.Message
            });
        }
    }
}