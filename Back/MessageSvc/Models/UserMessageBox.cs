﻿using GlobalModels.Messages.CompanyResponse;
using MongoDB.Entities;

namespace MessageSvc.Models;

public class UserMessageBox : Entity
{
    public Guid UserId { get; }
    public List<CompanyResponse> CompanyResponses { get; } 
    
    public UserMessageBox (Guid userId)
    {
        UserId = userId;
        CompanyResponses = new List<CompanyResponse>();
    }
}