using Contracts;
using Contracts.Events.Messages.CreatingBoxEvents;
using MassTransit;
using MessageSvc.Repositories.VacancyMessageBoxRepo;

namespace MessageSvc.Consumers;

public sealed class CreatingVacancyMessageBoxConsumer(IVacancyMessageBoxRepo vacancyMessageBoxRepo) : IConsumer<CreateVacancyMessageBoxEvent>
{
    public async Task Consume(ConsumeContext<CreateVacancyMessageBoxEvent> context)
    {
        try
        {
            await vacancyMessageBoxRepo.CreateMessageBox(
                vacancyId: context.Message.VacancyId,
                companyId: context.Message.CompanyId);
            
            var result = ServiceBusResultFactory.SuccessResult(true);
            await context.RespondAsync(result);
        }
        catch (Exception e)
        {
            var result = ServiceBusResultFactory.FailResult<bool>(e.Message);
            await context.RespondAsync(result);
        }
    }
}