using Calendars.Proxy.Core.Extensions;
using Calendars.Proxy.Core.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;
var isDevelopment = builder.Environment.IsDevelopment();

services.AddControllers();
services.AddCoreServices(config);

var authOptions = new AuthenticationServerOptions();
config.AssembleAuthenticationOptions(authOptions);
var webOptions = new WebServerOptions();
config.AssembleWebOptions(webOptions);

services.AddCors(opt =>
{
    opt.AddDefaultPolicy(cors =>
    {
        cors.AllowAnyHeader();
        cors.AllowAnyMethod();
        cors.WithOrigins(authOptions.Uri, webOptions.Uri);
    });
});

services.AddHttpClient();

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.Authority = authOptions.Uri;
        opt.IncludeErrorDetails = true;
        opt.RequireHttpsMetadata = isDevelopment == false;

        opt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false 
        };
    });
services.AddAuthorization();

var app = builder.Build();

app.UseRouting();

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();