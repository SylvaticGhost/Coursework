using AutoMapper;
using CompanySvc.Models;
using GlobalHelpers;

namespace CompanySvc.Helpers;

public class LocalValidator : Validation
{
    private readonly IMapper _mapper;
    
    public LocalValidator(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public static bool ValidateCompanyForm(CompanyToAddDto company)
    {
        if (ValidateEmail(company.Email) == false)
            return false;
        
        if (ValidatePhoneNumber(company.PhoneNumber) == false)
            return false;
        
        if (string.IsNullOrEmpty(company.Website) || string.IsNullOrEmpty(company.Name))
            return false;
        
        return true;
    }


    public bool ValidateCompanyForm(Company company)
    {
        CompanyToAddDto companyToAddDto = _mapper.Map<CompanyToAddDto>(company);
        
        return ValidateCompanyForm(companyToAddDto);
    }


    public bool ValidateCompanyForm(CompanyToUpdateDto company)
    {
        CompanyToAddDto companyToAddDto = _mapper.Map<CompanyToAddDto>(company);

        return ValidateCompanyForm(companyToAddDto);
    }
}