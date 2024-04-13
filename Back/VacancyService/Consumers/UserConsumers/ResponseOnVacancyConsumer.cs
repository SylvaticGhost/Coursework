using Contracts;
using CustomExceptions._400s;
using MassTransit;
using VacancyService.Repositories;

namespace VacancyService.Consumers.UserConsumers;

public sealed class ResponseOnVacancyConsumer(IVacancyRepo vacancyRepo, IVacancyResponseRepo responseRepo) : IConsumer<ResponseOnVacancyEvent>
{
    public async Task Consume(ConsumeContext<ResponseOnVacancyEvent> context)
    {
        IServiceBusResult<bool>? result = null;
        try
        {
            Guid vacancyId = context.Message.ResponseOnVacancy.VacancyId;

            if (!await vacancyRepo.CheckIfVacancyExists(vacancyId))
                throw new BadRequestException($"Vacancy aren't exist, vacancyId: {vacancyId}");

            await responseRepo.AddResponse(vacancyId, context.Message.ResponseOnVacancy);

            result = new ServiceBusResult<bool>
            {
                Result = true,
                IsSuccess = true
            };
        }
        catch (Exception ex) when (ex is BadRequestException)
        {
            result = new ServiceBusResult<bool>
            {
                Result = false,
                IsSuccess = false,
                ErrorMessage = "BadRequest Exception, " + ex.Message,
            };
        }
        catch (Exception ex)
        {
            result = new ServiceBusResult<bool>
            {
                Result = false,
                IsSuccess = false,
                ErrorMessage = "Internal Server Error, " + ex.Message,
            };
        }
        finally
        {
            await context.RespondAsync(result!);
        }

    }
}