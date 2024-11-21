using Microsoft.Extensions.DependencyInjection;
namespace Zeil.CreditCardValidation.Api.Client;


public static class ServiceExtensions
{
    public static void AddCreditCardValidationClient(this IServiceCollection services, CreditCardValidationOptions options)
    {
        services.AddHttpClient<ICreditCardValidationClient, CreditCardValidationClient>(client =>
        {
            client.BaseAddress = options.BaseAddress;
            client.DefaultRequestHeaders.Add("X-Api-Key", options.ApiKey);
        });
    }
}