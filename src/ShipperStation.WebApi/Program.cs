using ShipperStation.Application;
using ShipperStation.Infrastructure;
using ShipperStation.WebApi;
using System.Collections;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);

Console.WriteLine("==================================================================================");
await Task.Delay(1000);
foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
    Console.WriteLine("  {0} = {1}", de.Key, de.Value);
Console.WriteLine("==================================================================================");
foreach (var c in builder.Configuration.AsEnumerable())
{
    Console.WriteLine(c.Key + " = " + c.Value);
}
Console.WriteLine("==================================================================================");

var app = builder.Build();

await app.UseWebApplication();

app.Run();
