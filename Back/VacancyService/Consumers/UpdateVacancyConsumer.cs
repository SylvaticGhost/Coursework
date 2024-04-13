using Contracts;
using Contracts.VacancyEvent;
using CustomExceptions._400s;
using MassTransit;
using VacancyService.Repositories;

namespace VacancyService.Consumers;

public sealed class UpdateVacancyConsumer(IVacancyRepo vacancyRepo) : IConsumer<UpdateVacancyEvent>
{
    public async Task Consume(ConsumeContext<UpdateVacancyEvent> conext)
    {
        try
        {
            if(!await vacancyRepo.CheckIfVacancyExists(conext.Message.VacancyToUpdate.VacancyId))
                throw new BadRequestException($"Vacancy not found, id: {conext.Message.VacancyToUpdate.VacancyId}");
        
            bool companyOwnVacancy = 
                await vacancyRepo.CheckIfCompanyOwnVacancy(
                    companyId: conext.Message.CompanyId,vacancyId: conext.Message.VacancyToUpdate.VacancyId);
        
            if(!companyOwnVacancy)
                throw new ForbiddenException("Company cannot own this vacancy");
        
            await vacancyRepo.UpdateVacancy(conext.Message.VacancyToUpdate, conext.Message.Time);

            IServiceBusResult<bool> result = new ServiceBusResult<bool>
            {
                Result = true,
                IsSuccess = true
            };
            
            await conext.RespondAsync(result);
        }
        catch(Exception e)
        {
            IServiceBusResult<bool> result = new ServiceBusResult<bool>
            {
                Result = false,
                IsSuccess = false,
                ErrorMessage = e.Message + $"type: {e.GetType()}"
            };
            
            await conext.RespondAsync(result);
        }
    }
}