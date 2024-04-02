using System.Data;
using System.Text;
using AccountService.Data;
using AccountService.Repositories.UserProfile;
using GlobalHelpers;
using GlobalHelpers.DataHelpers.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;



var builder = WebApplication.CreateBuilder(args);

#region "Configuration"

ConsoleHelpers.WriteStartUpMessage("AccountService");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

string? tokenKeyString = builder.Configuration.GetSection("JWT:Key").Value;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetSection("Auth:Issuer").Value,
            ValidAudience = builder.Configuration.GetSection("Auth:Audience").Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                tokenKeyString ?? throw new InvalidOperationException("Token key not found")))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        b => b.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
    );
});

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
Console.WriteLine($"Connection string to npg: {connectionString}");

builder.Services.AddDbContext<DataContextNpgEf>(options =>
{
    options.UseNpgsql(connectionString);
});

MongoDbSettings? mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection(nameof(MongoDbSettings)));

if (mongoDbSettings is null)
   throw new DataException("MongoDbSettings not found in appsettings.json");

builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await MongoDbInit.InitDb(app, mongoDbSettings);

app.UseCors();
app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();
app.Run();

