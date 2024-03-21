using System.ComponentModel.DataAnnotations;
using AccountService.Data;
using AccountService.Models;
using AccountService.Repositories;
using GlobalHelpers;
using GlobalHelpers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AccountService.Controlers;

[ApiController]
[Route("[controller]")]
public class UserAuth : ControllerBase
{
    private readonly IUserRepository _userRepository;
    
    public UserAuth(DataContextEf dataContextEf, IConfiguration configuration)
    {
        _userRepository = new UserRepository(dataContextEf, configuration);
    }
    
    
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserAccountToAddDto userAccountToAddDto)
    {
        ValidationResults validationResult = userAccountToAddDto.Validate();
        
        if(!validationResult.Result)
            return new BadRequestObjectResult("Invalid input");
        
        Guid id = await _userRepository.AddUser(userAccountToAddDto);
        
        return new OkObjectResult(id);
    }
    
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserAccountToLoginDto userAccountToLoginDto)
    {
        if(!Validation.ValidateEmail(userAccountToLoginDto.Email))
            return new BadRequestObjectResult("Invalid email");
        
        if(string.IsNullOrWhiteSpace(userAccountToLoginDto.Password))
            return new BadRequestObjectResult("Password is required");

        string token;
        if(!string.IsNullOrWhiteSpace(userAccountToLoginDto.Email))
            token = await _userRepository.Login(userAccountToLoginDto.Email, userAccountToLoginDto.Password, TypeOfLogin.LoginByEmail);
        else
            token = await _userRepository.Login(userAccountToLoginDto.PhoneNumber!, userAccountToLoginDto.Password, TypeOfLogin.LoginByPhoneNumber);
        
        return new OkObjectResult(token);
    }
}