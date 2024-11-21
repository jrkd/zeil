using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Zeil.CreditCardValidation.Api;

/// <summary>
/// Options class to enable DI for IApiVersionDescriptionProvider to be able to generate all versions that exist for controllers. 
/// </summary>
/// <param name="apiVersionDescriptionProvider"></param>
public class MultiVersionSwaggerGenOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = $"Credit card validation api {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = $"An api to verify a credit card number is valid with the use of the [Luhn algorithm](https://en.wikipedia.org/wiki/Luhn_algorithm)",
                Contact = new() { Url = new Uri("https://zeil.com/"), Name = "Zeil" },
            });
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }
    }
}