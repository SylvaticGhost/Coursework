using AutoMapper;
using CompanySvc.Models;
using VacancyService.Models;

namespace CompanySvc.Helpers;

internal class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CompanyToAddDto, Company>();
        CreateMap<Company, CompanyShortInfo>();
        CreateMap<CompanyToAddDto, CompanyToUpdateDto>();
    }
}