using Calendars.Resources.Core.Extensions;
using Calendars.Resources.Data.Extensions;
using Calendars.Resources.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;
var env = builder.Environment;

services.AddControllers();

services.AddDataLayer(connectionString: config.GetSystemConnectionString());

services.AddCustomExceptionHandler();
services.AddResponseFactory();

var app = builder.Build();

app.UseCustomExceptionHandler();

app.UseRouting();

app.UseEndpoints((IEndpointRouteBuilder mapping) => mapping.MapControllers());

app.Run();