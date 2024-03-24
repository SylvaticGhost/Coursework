using AccountService.Models;
using GlobalHelpers.DataHelpers.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Entities;

namespace AccountService.Data;

internal static class MongoDbInit
{
    public static async Task InitDb(WebApplication app, MongoDbSettings mongoDbSettings)
    {
        await DB.InitAsync(mongoDbSettings.DatabaseName, 
            MongoClientSettings.FromConnectionString(mongoDbSettings.ConnectionString));
        
        await DB.Index<UserProfile>().Key(c => c.Id, KeyType.Ascending).CreateAsync();
        
        using var scope = app.Services.CreateScope();
        
    }
}