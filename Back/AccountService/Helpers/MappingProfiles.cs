using AccountService.Models;
using AutoMapper;

namespace AccountService.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserProfileToUpdateDto, UserProfileToAddDto>();
    }
}