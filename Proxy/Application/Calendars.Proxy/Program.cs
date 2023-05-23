using Calendars.Proxy.Core.Extensions;
using Calendars.Proxy.Core.Options;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;
var isDevelopment = builder.Environment.IsDevelopment();

services.AddControllers();
services.AddCoreServices(config);

var authOptions = new AuthenticationServerOptions();
config.AssembleAuthenticationOptions(authOptions);

services.AddAuthentication()
    .AddJwtBearer(opt =>
    {
        opt.Audience = authOptions.Uri;
        opt.Authority = authOptions.ClientId;
        opt.RequireHttpsMetadata = isDevelopment;
    });
services.AddAuthorization();

var app = builder.Build();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();