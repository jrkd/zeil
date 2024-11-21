using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Zeil.CreditCardValidation.Api;
using Zeil.CreditCardValidation.Api.Services.Interfaces;


var configuration = new ConfigurationBuilder()
           .SetBasePath(AppContext.BaseDirectory)
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

var builder = WebApplication.CreateBuilder(args);

//Setup DI services
builder.Services.AddSingleton<ILuhnValidationService, LuhnValidationService>();
builder.Services.Configure<ApiAuthenticationOptions>(configuration.GetSection(nameof(ApiAuthenticationOptions)));
builder.Services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, MultiVersionSwaggerGenOptions>();
builder.Services.AddSingleton<IConfigureOptions<SwaggerUIOptions>, MultiVersionSwaggerUIOptions>();

//Setup api authentication scheme
builder.Services.AddAuthentication("ApiKeyScheme")
    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKeyScheme", options => { });
builder.Services.AddAuthorization();

//Setup api/controller/swagger spec
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(2, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().WithOpenApi();

app.Run();