//http://localhost:5240/swagger/index.html
using System.Data;
using CompanySvc.Consumers;
using CompanySvc.Data;
using CompanySvc.Repositories;
using GlobalHelpers;
using GlobalHelpers.DataHelpers.Models;
using MassTransit;


ConsoleHelpers.WriteStartUpMessage("CompanySvc");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    
    x.AddConsumer<GetCompanyShortInfo>();
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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        b => b.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
    );
});

MongoDbSettings? mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection(nameof(MongoDbSettings)));

if (mongoDbSettings is null)
    throw new DataException("MongoDbSettings not found in appsettings.json");

builder.Services.AddScoped<ICompanyRepo, CompanyRepo>();



var app = builder.Build();

await MongoDbInit.InitDb(app, mongoDbSettings);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseRouting();
app.MapControllers();

app.UseHttpsRedirection();

app.Run();



















