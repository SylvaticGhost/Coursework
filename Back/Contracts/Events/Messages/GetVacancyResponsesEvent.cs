using Contracts.Events.Messages;

namespace Contracts.Events.ResponseOnVacancyEvents;

public record GetVacancyResponsesEvent(Guid VacancyId, Guid CompanyId) : Event;