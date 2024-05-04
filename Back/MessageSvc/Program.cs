using System.Data;
using Contracts.Events.Messages;
using GlobalHelpers.DataHelpers.Models;
using MassTransit;
using MessageSvc;
using MessageSvc.Consumers;
using MessageSvc.Consumers.ApplicationOnVacancy;
using MessageSvc.Consumers.UserMessages;
using MessageSvc.Data;
using MessageSvc.Repositories.UserMessageBoxRepo;
using MessageSvc.Repositories.VacancyMessageBoxRepo;
using GetUserApplicationsConsumer = MessageSvc.Consumers.ApplicationOnVacancy.GetUserApplicationsConsumer;


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddScoped<IUserMessageBoxRepo, UserMessageBoxRepo>();
builder.Services.AddScoped<IVacancyMessageBoxRepo, VacancyMessageBoxRepo>();

builder.Services.AddMassTransit(x => 
{
    x.SetKebabCaseEndpointNameFormatter();
    
    x.AddConsumer<CreatingUserMessageBoxConsumer>();
    x.AddConsumer<CreatingVacancyMessageBoxConsumer>();
    
    x.AddConsumer<ApplicationForVacancyPostConsumer>();
    
    x.AddConsumer<GetUserApplicationsConsumer>();
    x.AddConsumer<GetUserApplicationOnVacancyConsumer>();
    x.AddConsumer<DeleteApplicationConsumer>();
    
    x.AddConsumer<CheckIfUserAppliedConsumer>();

    x.AddConsumer<UserMessagesConsumer>();
    
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



MongoDbSettings? mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection(nameof(MongoDbSettings)));

if (mongoDbSettings is null)
    throw new DataException("MongoDbSettings not found in appsettings.json");

await MongoDbInit.InitDb(mongoDbSettings);

var host = builder.Build();
host.Run();
