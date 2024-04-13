using Contracts;
using Contracts.Events.ResponseOnVacancyEvents;
using CustomExceptions._400s;
using GlobalModels;
using MassTransit;
using VacancyService.Repositories;

namespace VacancyService.Consumers.UserConsumers;

public sealed class GetResponsesOnVacancyConsumer(IVacancyResponseRepo vacancyResponseRepo, IVacancyRepo vacancyRepo) 
    : IConsumer<GetVacancyResponsesEvent>
{
    public async Task Consume(ConsumeContext<GetVacancyResponsesEvent> context)
    {
        IServiceBusResult<IEnumerable<ResponseOnVacancy>>? result = null;
        try
        {
            if (!await vacancyRepo.CheckIfVacancyExists(context.Message.VacancyId))
                throw new BadRequestException($"Vacancy not found, vacancy id: {context.Message.VacancyId}");

            if (!await vacancyRepo.CheckIfCompanyOwnVacancy(companyId: context.Message.CompanyId,
                    vacancyId: context.Message.VacancyId))
                throw new ForbiddenException($"""
                                              Company aren't owner of this vacancy\n
                                              CompanyId: {context.Message.CompanyId},
                                              VacancyId: {context.Message.VacancyId}
                                              """);

            IEnumerable<ResponseOnVacancy>? responses =
                await vacancyResponseRepo.GetResponses(context.Message.VacancyId);

            result = new ServiceBusResult<IEnumerable<ResponseOnVacancy>>
            {
                IsSuccess = true,
                Result = responses
            };
        }
        catch (Exception ex) when (ex is BadRequestException or ForbiddenException)
        {
            result = new ServiceBusResult<IEnumerable<ResponseOnVacancy>>
            {
                IsSuccess = false,
                ErrorMessage = ex.Message
            };
        }
        catch (Exception ex)
        {
            result = new ServiceBusResult<IEnumerable<ResponseOnVacancy>>
            {
                IsSuccess = false,
                ErrorMessage = "Internal server" + ex.Message
            };
        }
        finally
        {
            await context.RespondAsync(result!);
        }
      
    }
}

