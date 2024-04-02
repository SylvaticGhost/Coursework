using CompanySvc.Models;
using GlobalHelpers.DataHelpers.Models;
using MongoDB.Driver;
using MongoDB.Entities;

namespace CompanySvc.Data;

internal class MongoDbInit
{
    public static async Task InitDb(WebApplication app, MongoDbSettings mongoDbSettings)
    {
        Console.WriteLine($"MongoDbSettings: {mongoDbSettings.ConnectionString}, {mongoDbSettings.DatabaseName}");
        await DB.InitAsync(mongoDbSettings.DatabaseName, 
            MongoClientSettings.FromConnectionString(mongoDbSettings.ConnectionString));

        await DB.Index<Company>()
            .Key(c => c.CompanyId, KeyType.Ascending)
            .CreateAsync();
        
        //using var scope = app.Services.CreateScope();
        
    }
}