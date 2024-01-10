using ShipperStation.Application;
using ShipperStation.Infrastructure;
using ShipperStation.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);

var app = builder.Build();

await app.UseWebApplication();

app.Run();
