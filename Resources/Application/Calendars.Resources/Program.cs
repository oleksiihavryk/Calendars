using Calendars.Resources.Core.Extensions;
using Calendars.Resources.Data.Extensions;
using Calendars.Resources.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;
var env = builder.Environment;

services.AddControllers();

services.AddDataLayer(connectionString: config.GetSystemConnectionString());

services.AddCustomExceptionHandler();
services.AddResponseFactory();
services.AddAutoMapper();

services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Calendars API",
        Description = "Swagger API explorer of Calendars resources API.",
        Version = "v1"
    });
});
services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseRouting();

app.UseCustomExceptionHandler();
//if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calendar API V1");
    c.RoutePrefix = string.Empty;
});

app.UseEndpoints((IEndpointRouteBuilder mapping) => mapping.MapControllers());

app.Run();