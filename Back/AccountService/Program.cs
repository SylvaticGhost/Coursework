using System.Data;
using System.Text;
using AccountService.Data;
using AccountService.Repositories;
using AccountService.Repositories.UserProfile;
using GlobalHelpers;
using GlobalHelpers.DataHelpers.Models;
using MassTransit;
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

string? tokenKeyString = builder.Configuration.GetSection("Jwt:Key").Value;
Console.WriteLine($"Token key: {tokenKeyString}");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
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

//To ensure that rabbitmq will be up and is running
await Task.Delay(1000);

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", h =>
        {
            h.Username(builder.Configuration.GetValue("RabbitMq:UserName", "myuser"));
            h.Password(builder.Configuration.GetValue("RabbitMq:Password", "mypass"));
        });
    });
});

builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRepositoryBasic, UserProfileBasicInfoRepo>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseHttpsRedirection();
app.Run();

