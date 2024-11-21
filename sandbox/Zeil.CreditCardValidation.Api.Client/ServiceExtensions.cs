using Microsoft.Extensions.DependencyInjection;
using Zeil.CreidtCardValidation.Api.Client;

namespace Zeil.CreditCardValidation.Api.Client;


public static class ServiceExtensions
{
    public static void AddCreditCardValidationClient(this IServiceCollection services, CreditCardValidationOptions options)
    {
        services.AddHttpClient<ICreditCardValidationClient, CreditCardValidationClient>(client =>
        {
            client.BaseAddress = options.BaseAddress;
            //TODO: include auth
        });
    }
}