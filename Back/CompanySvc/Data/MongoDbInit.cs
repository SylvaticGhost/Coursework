using System.Data.Common;
using AccountService.Models;
using CompanySvc.Models;
using GlobalHelpers.DataHelpers.Models;
using MongoDB.Driver;
using MongoDB.Entities;

namespace CompanySvc.Data;

internal static class MongoDbInit
{
    public static async Task InitDb(WebApplication app, MongoDbSettings mongoDbSettings)
    {
        await DB.InitAsync(mongoDbSettings.DatabaseName, 
            MongoClientSettings.FromConnectionString(mongoDbSettings.ConnectionString));
        
        await DB.Index<Company>().Key(c => c.Id, KeyType.Ascending).CreateAsync();
        
        using var scope = app.Services.CreateScope();
        
    }
}