using Calendars.Resources.Core;
using Calendars.Resources.Core.Extensions;
using Calendars.Resources.Data.Extensions;
using Calendars.Resources.Extensions;
using Calendars.Resources.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;
var env = builder.Environment;

services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

services.AddModelStateValidatingGlobalFilter();

services.AddControllers(opt =>
{
    opt.Filters.AddService<CustomModelStateActionFilter>();
});

services.AddDataLayer(connectionString: config.GetSystemConnectionString());

services.AddCustomExceptionHandler();
services.AddResponseFactory();
services.AddAutoMapper();

AuthenticationConfiguration authCfg = config.AssembleAuthenticationConfiguration();

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.Audience = authCfg.Audience;
        opt.Authority = authCfg.Uri.ToString();
        opt.RequireHttpsMetadata = env.IsDevelopment() == false;
    });
services.AddAuthorization();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Calendars API",
        Description = "Swagger API explorer of Calendars resources API.",
        Version = "v1"
    });
    opt.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        In = ParameterLocation.Header,
        BearerFormat = "JWT",
        Flows = new OpenApiOAuthFlows()
        {
            ClientCredentials = new OpenApiOAuthFlow()
            {
                AuthorizationUrl = new Uri(uriString:
                    authCfg.Uri + "connect/authorize"),
                TokenUrl = new Uri(uriString:
                    authCfg.Uri + "connect/token"),
                Scopes =
                {
                    [authCfg.Scope] =
                        "Default scope for resource server security."
                },

            }
        },
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        [new OpenApiSecurityScheme()
        {
            Reference = new OpenApiReference()
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme,
            },
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            In = ParameterLocation.Header
        }] = new string[] { }
    });
});

var app = builder.Build();

app.UseRouting();

app.UseHttpsRedirection();
app.UseCustomExceptionHandler();

if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calendar API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints((IEndpointRouteBuilder mapping) => mapping.MapControllers());

app.Run();