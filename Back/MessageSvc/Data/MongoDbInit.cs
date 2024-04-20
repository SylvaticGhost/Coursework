using GlobalHelpers.DataHelpers.Models;
using MessageSvc.Models;
using MongoDB.Driver;
using MongoDB.Entities;

namespace MessageSvc.Data;

/// <summary>
/// Inner class for MongoDb in MessageSvc
/// </summary>
internal static class MongoDbInit
{
    public static async Task InitDb(MongoDbSettings settings)
    {
        await DB.InitAsync(settings.DatabaseName, MongoClientSettings.FromConnectionString(settings.ConnectionString));
        
        await DB.Index<VacancyApplicationsBox>().Key(c => c.VacancyId, KeyType.Ascending).CreateAsync();
        
        await DB.Index<UserMessageBox>().Key(v => v.UserId, KeyType.Ascending).CreateAsync();
    }
}