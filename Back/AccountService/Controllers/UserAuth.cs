﻿//http://localhost:5239/swagger/index.html

using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AccountService.Data;
using AccountService.Models;
using AccountService.Repositories;
using CustomExceptions;
using CustomExceptions._500s_exceptions;
using GlobalHelpers;
using GlobalHelpers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
[DefaultExceptionFilter]
public class UserAuth : ControllerBase
{
    private readonly IUserRepository _userRepository;
    
    public UserAuth(DataContextNpgEf dataContextNpgEf, IConfiguration configuration)
    {
        _userRepository = new UserRepository(dataContextNpgEf, configuration);
    }
    
    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserAccountToAddDto userAccountToAddDto)
    {
        ValidationResults validationResult = userAccountToAddDto.Validate();
        
        if(await _userRepository.CheckIfEmailExists(userAccountToAddDto.Email))
            validationResult.AddError("Email already exists");
        
        if(await _userRepository.CheckIfPhoneNumberExists(userAccountToAddDto.PhoneNumber))
            validationResult.AddError("Phone number already exists");
        
        if(!validationResult.Result)
            return new BadRequestObjectResult("Invalid input");
        
        Guid id = await _userRepository.AddUser(userAccountToAddDto);
        
        return new OkObjectResult(id);
    }
    
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserAccountToLoginDto userAccountToLoginDto)
    {
        if(string.IsNullOrWhiteSpace(userAccountToLoginDto.Email) 
           && string.IsNullOrWhiteSpace(userAccountToLoginDto.PhoneNumber))
            return new BadRequestObjectResult("Email or phone number is required");
        
        if(!Validation.ValidateEmail(userAccountToLoginDto.Email))
            return new BadRequestObjectResult("Invalid email");
        
        if(string.IsNullOrWhiteSpace(userAccountToLoginDto.Password))
            return new BadRequestObjectResult("Password is required");
        
        if(await _userRepository.CheckIfEmailExists(userAccountToLoginDto.Email) 
           && await _userRepository.CheckIfPhoneNumberExists(userAccountToLoginDto.PhoneNumber!))
            return new BadRequestObjectResult("Invalid login or password");

        string token;
        try
        {
            if(!string.IsNullOrWhiteSpace(userAccountToLoginDto.Email))
                token = await _userRepository.Login(
                    userAccountToLoginDto.Email, userAccountToLoginDto.Password, TypeOfLogin.LoginByEmail);
            else
                token = await _userRepository.Login(
                    userAccountToLoginDto.PhoneNumber!, userAccountToLoginDto.Password, TypeOfLogin.LoginByPhoneNumber);
        }
        catch (ArgumentException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
        return new OkObjectResult(token);
    }
    
    
    [Authorize]
    [HttpGet("RefreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        try
        {
            Guid id = Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
        
            if(id == Guid.Empty)
                return new BadRequestObjectResult("Invalid user");
            Console.WriteLine("id: " + id);
            
            string newToken = await _userRepository.RefreshToken(id);
            Console.WriteLine("newToken: " + newToken);
        
            return new OkObjectResult(newToken);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
    
    
    [Authorize]
    [HttpPost("DeleteAccount")]
    public async Task<IActionResult> DeleteAccount()
    {
        Guid id = Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
        
        if(id == Guid.Empty)
            return new BadRequestObjectResult("Invalid user");
        
        if(await _userRepository.DeleteUser(id))
            return new OkObjectResult("Account deleted");
        
        return StatusCode(StatusCodes.Status500InternalServerError, "Account not deleted");
    }
    
    
    [Authorize]
    [HttpGet("TestAuth")]
    public IActionResult TestAuth()
    {
        try
        {
            Guid id = Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
            return new OkObjectResult("You are authorized, id " + id);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
}