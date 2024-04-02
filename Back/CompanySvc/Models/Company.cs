﻿using System.ComponentModel.DataAnnotations;
using MongoDB.Entities;

namespace CompanySvc.Models;

public class Company : Entity
{
    [Required]
    public Guid CompanyId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Website { get; set; }
    
    public byte[]? Logo { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public int NumberOfEmployees { get; set; }
    
    public string Description { get; set; }
    
    public string Industry { get; set; }
}