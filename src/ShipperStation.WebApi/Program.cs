using Hangfire;
using ShipperStation.Application;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Infrastructure;
using ShipperStation.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);

var app = builder.Build();

await app.UseWebApplication();

RecurringJob.AddOrUpdate<IPackageService>("push-notify", service => service.PushNotifyPackage(), Cron.Daily(9));

app.Run();
