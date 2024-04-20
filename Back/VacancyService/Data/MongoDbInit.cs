using GlobalHelpers.DataHelpers.Models;
using GlobalModels;
using GlobalModels.Vacancy;
using MongoDB.Driver;
using MongoDB.Entities;
using VacancyService.Models;

namespace VacancyService.Data;

internal static class MongoDbInit
{
    public static async Task InitDb(WebApplication app, MongoDbSettings mongoDbSettings)
    {
        await DB.InitAsync(mongoDbSettings.DatabaseName, 
            MongoClientSettings.FromConnectionString(mongoDbSettings.ConnectionString));
        
        await DB.Index<Vacancy>().Key(c => c.VacancyId, KeyType.Ascending).CreateAsync();
    }
}