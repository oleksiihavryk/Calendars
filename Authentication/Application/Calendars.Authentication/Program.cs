using Calendars.Authentication.Core.Extensions;
using Calendars.Authentication.Data.Extensions;
using Calendars.Authentication.Extensions;
using Calendars.Authentication.Options;
using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var env = builder.Environment;
var config = builder.Configuration;

//project configuration
services.Configure<RouteOptions>(opt =>
{
    opt.AppendTrailingSlash = true;
    opt.LowercaseUrls = true;
});
builder.Services.Configure<SecurityStampValidatorOptions>(opts =>
{
    opts.OnRefreshingPrincipal = SecurityStampValidatorCallback.UpdatePrincipal;
});
services.Configure<CancelUrlOptions>(opt =>
{
    opt.Url = config.GetOneOfWebClientUrlsOrReturnNull() ?? throw new ApplicationException(
            message: "Configuration file is incorrect! " +
                 "Cannot accept information about web client origin form configuration file.");
});

services.AddMvc(opt => opt.EnableEndpointRouting = false);

services.AddResponseFactory();

services.AddDataLayer(config.GetAuthenticationConnectionString());

var isConfig = config
    .AssembleIdentityServerInMemoryConfiguration(
        isDevelopment: env.IsDevelopment());

services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
        policy.DisallowCredentials();
        policy.WithOrigins(isConfig.ClientsOrigins);
    });
});
services.AddIdentityServer(isConfig);

//build app
var app = builder.Build();
//pollute roles
await app.SeedRolesAsync();

//middleware chain
app.UseRouting();

app.UseCors();
app.UseHttpsRedirection();

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseStatusCodePages();
}

app.UseStaticFiles();

app.UseIdentityServer();
app.UseMvc(routes =>
{
    routes.MapRoute(
        name: null,
        template: "{controller}/{action}");
});

app.Run();