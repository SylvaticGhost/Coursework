namespace GlobalHelpers.DataHelpers.Models;

public record MongoDbSettings(
    string ConnectionString,
    string DatabaseName
);