using Contracts.Events.Messages;

namespace Contracts.Events.ResponseOnVacancyEvents;

public record GetVacancyApplicationEvent(Guid VacancyId, Guid CompanyId) : Event;