using System.Data;
using VacancyService.Repositories;
using GlobalHelpers.DataHelpers.Models;
using MassTransit;
using VacancyService.Consumers;
using VacancyService.Data;
using VacancyService.Helpers;
using VacancyService.SearchContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    
    x.AddConsumer<UpdateCompanyConsumer>();
    x.AddConsumer<DeleteCompanyConsumer>();
    
    x.AddConsumer<AddVacancyConsumer>();
    x.AddConsumer<DeleteVacancyConsumer>();
    x.AddConsumer<UpdateVacancyConsumer>();
    
    x.AddConsumer<GetCompanyVacanciesConsumer>();
    
    x.AddConsumer<CheckIfVacancyExistConsumer>();
    x.AddConsumer<GetOwnerOfVacancyConsumer>();
    
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

builder.Services.AddScoped<IVacancyRepo, VacancyRepo>();
builder.Services.AddScoped<ISearchVacancyContext, SearchVacancyContext>();

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

