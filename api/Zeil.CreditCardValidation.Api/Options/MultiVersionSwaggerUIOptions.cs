using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Zeil.CreditCardValidation.Api;

/// <summary>
/// Options class to enable DI for IApiVersionDescriptionProvider to be able to generate all versions that exist for controllers. 
/// </summary>
/// <param name="apiVersionDescriptionProvider"></param>
public class MultiVersionSwaggerUIOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider) : IConfigureOptions<SwaggerUIOptions>
{
    public void Configure(SwaggerUIOptions options)
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
        }
    }
}