using AutoMapper;
using GlobalModels.Vacancy;

namespace VacancyService.Helpers;

internal class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Vacancy, VacancyToAddDto>();
        CreateMap<Vacancy, VacancyDto>();
    }   
}