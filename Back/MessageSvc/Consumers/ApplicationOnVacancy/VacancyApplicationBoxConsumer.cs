using Contracts;
using MassTransit;

namespace MessageSvc.Consumers.ApplicationOnVacancy;

public class VacancyApplicationBoxConsumer : IConsumer<AddVacancyEvent>
{
    public async Task Consume(ConsumeContext<AddVacancyEvent> context)
    {
        
    }
}