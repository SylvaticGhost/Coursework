using Contracts;
using MassTransit;
using MessageSvc.Repositories.VacancyMessageBoxRepo;

namespace MessageSvc.Consumers;

public sealed class CreatingVacancyMessageBoxConsumer(IVacancyMessageBoxRepo vacancyMessageBoxRepo) : IConsumer<AddVacancyEvent>
{
    public async Task Consume(ConsumeContext<AddVacancyEvent> context)
    {
        try
        {
            await vacancyMessageBoxRepo.CreateMessageBox(context.Message.CompanyShortInfo.Id);
            var result = ServiceBusResultBuilder.Success(true);
            await context.RespondAsync(result);
        }
        catch (Exception e)
        {
            var result = ServiceBusResultBuilder.Fail<bool>(e.Message);
            await context.RespondAsync(result);
        }
        
        throw new NotImplementedException();
    }
}