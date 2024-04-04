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
        Console.WriteLine($"MongoDbSettings: {mongoDbSettings.ConnectionString}, {mongoDbSettings.DatabaseName}");
        await DB.InitAsync(mongoDbSettings.DatabaseName, 
            MongoClientSettings.FromConnectionString(mongoDbSettings.ConnectionString));
        
        await DB.Index<UserProfile>().Key(c => c.UserId, KeyType.Ascending).CreateAsync();
    }
}