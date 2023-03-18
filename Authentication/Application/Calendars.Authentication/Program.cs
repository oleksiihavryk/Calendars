using Calendars.Authentication.Core.Extensions;
using Calendars.Authentication.Data.Extensions;
using Calendars.Authentication.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var env = builder.Environment;
var config = builder.Configuration;

services.Configure<RouteOptions>(opt =>
{
    opt.AppendTrailingSlash = true;
    opt.LowercaseUrls = true;
});

services.AddMvc(opt => opt.EnableEndpointRouting = false);

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

var app = builder.Build();

app.UseRouting();

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