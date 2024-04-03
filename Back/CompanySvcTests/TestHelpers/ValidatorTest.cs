using Moq;
using AutoMapper;
using CompanySvc.Models;
using CompanySvc.Helpers;

[TestFixture]
public class LocalValidatorTests
{
    private Mock<IMapper> _mockMapper;
    private LocalValidator _validator;

    [SetUp]
    public void Setup()
    {
        _mockMapper = new Mock<IMapper>();
        _validator = new LocalValidator(_mockMapper.Object);
    }

    [Test]
    public void ValidateCompanyForm_ReturnsTrue_WhenCompanyToAddDtoIsValid()
    {
        var companyToAddDto = new CompanyToAddDto(
            Email: "test@test.com",
            PhoneNumber: "1212342432",
            Website: "www.test.com",
            Name: "Test",
            Address: "Horina, 16",
            Description: "company",
            Industry: string.Empty,
            Logo: null
        );
        

        var result = LocalValidator.ValidateCompanyForm(companyToAddDto);

        Assert.IsTrue(result);
    }

    [Test]
    public void ValidateCompanyForm_ReturnsFalse_WhenCompanyToAddDtoIsInvalid()
    {
        var companyToAddDto = new CompanyToAddDto(
            Email: "inavlid",
            PhoneNumber: "56148487",
            Website: "www.test.com",
            Name: "Test",
            Address: "Horina, 16",
            Description: "company",
            Industry: string.Empty,
            Logo: null
        );

        var result = LocalValidator.ValidateCompanyForm(companyToAddDto);

        Assert.IsFalse(result);
    }

    [Test]
    public void ValidateCompanyForm_ReturnsTrue_WhenCompanyIsValid()
    {
        var company = new Company
        {
            Email = "test@test.com",
            PhoneNumber = "1234567890",
            Website = "www.test.com",
            Name = "Test",
            Address = "Horina 16",
            Description = "c",
            Industry = string.Empty,
            Logo = null
        };

        _mockMapper.Setup(m => m.Map<CompanyToAddDto>(It.IsAny<Company>())).Returns(
            new CompanyToAddDto(
            
                Email: company.Email,
                PhoneNumber : company.PhoneNumber,
                Website : company.Website,
                Name : company.Name,
                Address: company.Address,
                Description: company.Description,
                Logo: company.Logo,
                Industry: company.Industry
            )
            );

        var result = _validator.ValidateCompanyForm(company);

        Assert.IsTrue(result);
    }

    [Test]
    public void ValidateCompanyForm_ReturnsFalse_WhenCompanyIsInvalid()
    {
        var company = new Company
        {
            Email = "invalidEmail",
            PhoneNumber = "invalidPhoneNumber",
            Website = "",
            Name = "",
            Address = "Horina 16",
            Description = "c",
            Industry = string.Empty,
            Logo = null
        };

        _mockMapper.Setup(m => m.Map<CompanyToAddDto>(It.IsAny<Company>())).Returns(
            new CompanyToAddDto(
            
                Email: company.Email,
                PhoneNumber : company.PhoneNumber,
                Website : company.Website,
                Name : company.Name,
                Address: company.Address,
                Description: company.Description,
                Logo: company.Logo,
                Industry: company.Industry
            ));

        var result = _validator.ValidateCompanyForm(company);

        Assert.IsFalse(result);
    }
    

    [Test]
    public void ValidateCompanyForm_ReturnsTrue_WhenCompanyToUpdateDtoIsValid()
    {
        var companyToUpdateDto = new CompanyToUpdateDto(
            Guid.NewGuid(),
            "Test",
            "Horina, 16",
            "test@test.com",
            "1234567890",
            "www.test.com",
            null,
            100,
            "company",
            string.Empty
        );

        _mockMapper.Setup(m => m.Map<CompanyToAddDto>(It.IsAny<CompanyToUpdateDto>())).Returns(
            new CompanyToAddDto(
                Email: companyToUpdateDto.Email,
                Name: companyToUpdateDto.Name,
                Address: companyToUpdateDto.Address,
                PhoneNumber: companyToUpdateDto.PhoneNumber,
                Website: companyToUpdateDto.Website,
                Logo: companyToUpdateDto.Logo,
                Description: companyToUpdateDto.Description,
                Industry: companyToUpdateDto.Industry)
            );

        var result = _validator.ValidateCompanyForm(companyToUpdateDto);

        Assert.IsTrue(result);
    }

    [Test]
    public void ValidateCompanyForm_ReturnsFalse_WhenCompanyToUpdateDtoIsInvalid()
    {
        var companyToUpdateDto = new CompanyToUpdateDto(
            Guid.NewGuid(),
            "",
            "Some address",
            "invalidEmail",
            "invalidPhoneNumber",
            "",
            null,
            100,
            "Some description",
            "Some industry"
        );

        _mockMapper.Setup(m => m.Map<CompanyToAddDto>(It.IsAny<CompanyToUpdateDto>())).Returns(
            new CompanyToAddDto(
                Email: companyToUpdateDto.Email,
                Name: companyToUpdateDto.Name,
                Address: companyToUpdateDto.Address,
                PhoneNumber: companyToUpdateDto.PhoneNumber,
                Website: companyToUpdateDto.Website,
                Logo: companyToUpdateDto.Logo,
                Description: companyToUpdateDto.Description,
                Industry: companyToUpdateDto.Industry)
            );

        var result = _validator.ValidateCompanyForm(companyToUpdateDto);

        Assert.IsFalse(result);
    }
}
