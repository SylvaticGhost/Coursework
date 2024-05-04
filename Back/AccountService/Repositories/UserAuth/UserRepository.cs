﻿using AccountService.Data;
using AccountService.Helpers;
using AccountService.Models;
using Contracts.Events.Messages.CreatingBoxEvents;
using GlobalHelpers;
using GlobalHelpers.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Repositories;

public class UserRepository(DataContextNpgEf dataContextNpgEf, 
    IConfiguration configuration)
    : IUserRepository, IUserRepositoryBasic
{
    private readonly AuthHelpers _authHelpers = new(configuration);
    
    public async Task<Guid> AddUser(UserAccountToAddDto userAccountToAddDto)
    {
        HashedPasswords password = GlobalAuthHelpers.CreatePasswordHash(userAccountToAddDto.Password);
        
        UserAccount userAccount = new()
        {
            FirstName = userAccountToAddDto.FirstName,
            LastName = userAccountToAddDto.LastName,
            Email = userAccountToAddDto.Email,
            PhoneNumber = userAccountToAddDto.PhoneNumber,
            DateOfBirth = userAccountToAddDto.DateOfBirth,
            PasswordHash = password.PasswordHash,
            PasswordSalt = password.PasswordSalt
        };
        
        await dataContextNpgEf.UserAccount.AddAsync(userAccount);
        await dataContextNpgEf.SaveChangesAsync();
        
        return userAccount.Id;
    }
    
    
    public async Task<UserAccount?> GetUserByEmail(string email) => 
        await dataContextNpgEf.UserAccount.FirstOrDefaultAsync(x => x.Email == email);
    
    public async Task<UserAccount?> GetUserById(Guid id) =>
        await dataContextNpgEf.UserAccount.FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<UserAccount?> GetUserByPhoneNumber(string phoneNumber) =>
        await dataContextNpgEf.UserAccount.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    
    
    public async Task<string> Login(string credential, string password, TypeOfLogin typeOfLogin)
    {
        UserAccount? user;
        if (typeOfLogin == TypeOfLogin.LoginByEmail)
            user = await GetUserByEmail(credential);
        else
            user = await GetUserByPhoneNumber(credential);
        
        if (user == null)
            throw new ArgumentException("User not found");
        
        if(string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password is required");
        
        if (!GlobalAuthHelpers.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new ArgumentException("Invalid password");
        
        string token = _authHelpers.GenerateJwtToken(user);
        
        return token;
    }
    
    
    public async Task<string> RefreshToken(Guid id)
    {
        UserAccount? user = await dataContextNpgEf.UserAccount.FirstOrDefaultAsync(x => x.Id == id);
        
        if (user == null)
            throw new ArgumentException("User not found");
        
        string token = _authHelpers.GenerateJwtToken(user);
        
        return token;
    }


    public async Task<bool> DeleteUser(Guid id)
    {
        UserAccount? user = await GetUserById(id);
        
        if (user == null)
            throw new ArgumentException("User not found");
        
        dataContextNpgEf.UserAccount.Remove(user);
        
        await dataContextNpgEf.SaveChangesAsync();
        
        return true;
    }
    
    
    public async Task<bool> CheckIfEmailExists(string email) =>
        await dataContextNpgEf.UserAccount.AnyAsync(x => x.Email == email);
    
    
    public async Task<bool> CheckIfPhoneNumberExists(string phoneNumber) =>
        await dataContextNpgEf.UserAccount.AnyAsync(x => x.PhoneNumber == phoneNumber);

    public Task<(string FirstName, string? LastName)> GetNameAndSurname(Guid id)
    {
        return dataContextNpgEf.UserAccount
            .Where(x => x.Id == id)
            .Select(x => new {x.FirstName, x.LastName})
            .FirstOrDefaultAsync()
            .ContinueWith(x => (x.Result!.FirstName, x.Result.LastName));
    }


    public async Task UpdatePassword(string newPassword, Guid userId) 
    {
        UserAccount? user = await GetUserById(userId) ?? throw new ArgumentException("User not found");

        if (string.IsNullOrWhiteSpace(newPassword))
            throw new ArgumentException("Password is required");

        HashedPasswords password = GlobalAuthHelpers.CreatePasswordHash(newPassword);

        user.PasswordHash = password.PasswordHash;
        user.PasswordSalt = password.PasswordSalt;

        await dataContextNpgEf.SaveChangesAsync();
    }


    public async Task UpdateEmail(string newEmail, Guid userId)
    {
        UserAccount? user = await GetUserById(userId) ?? throw new ArgumentException("User not found");

        if (string.IsNullOrWhiteSpace(newEmail))
            throw new ArgumentException("Email is required");

        user.Email = newEmail;

        await dataContextNpgEf.SaveChangesAsync();
    }


    public async Task UpdatePhoneNumber(string newPhoneNumber, Guid userId)
    {
        UserAccount? user = await GetUserById(userId) ?? throw new ArgumentException("User not found");

        if (string.IsNullOrWhiteSpace(newPhoneNumber))
            throw new ArgumentException("Phone number is required");

        user.PhoneNumber = newPhoneNumber;

        await dataContextNpgEf.SaveChangesAsync();
    }


    public async Task UpdateName(Guid userId,string newFirstName = "", string newLastName = "") {
        UserAccount? user = await GetUserById(userId) ?? throw new ArgumentException("User not found");

        if (string.IsNullOrWhiteSpace(newFirstName) && string.IsNullOrWhiteSpace(newLastName))
            throw new ArgumentException("At least one of the fields is required");

        if (!string.IsNullOrWhiteSpace(newFirstName))
            user.FirstName = newFirstName;

        if (!string.IsNullOrWhiteSpace(newLastName))
            user.LastName = newLastName;

        await dataContextNpgEf.SaveChangesAsync();  
    }
}