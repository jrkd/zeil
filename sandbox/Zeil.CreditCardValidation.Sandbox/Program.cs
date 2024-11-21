using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zeil.CreditCardValidation.Api.Client;


var configuration = new ConfigurationBuilder()
           .SetBasePath(AppContext.BaseDirectory)
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<Sandbox>();

builder.Services.AddCreditCardValidationClient(configuration.GetSection(nameof(CreditCardValidationOptions)).Get<CreditCardValidationOptions>() ?? new());

using IHost host = builder.Build();

await host.Services.GetRequiredService<Sandbox>().Run();

await host.RunAsync();


public class Sandbox(ICreditCardValidationClient creditCardValidationClient)
{
    public async Task Run()
    {
        Console.WriteLine("===Sandbox example for https://github.com/jrkd/zeil credit card validation service.===");
        while (true)
        {
            Console.WriteLine("Please input the credit card number to validate");
            var cardNumber = Console.ReadLine();

            var validationResult = await creditCardValidationClient.CreditCardValidationAsync(new()
            {
                CardNumber = cardNumber
            });

            //Version 1 of the api example (requires changing the nswag.json to point to the v1 json)
            // try
            // {
            //     await creditCardValidationClient.CreditCardValidationAsync(cardNumber);
            //     Console.WriteLine("☑️ The card number PASSES validation.");
            // }
            // catch (ApiException)
            // {
            //     Console.WriteLine("🚫 The card number FAILS validation.");
            // }

            if (validationResult?.IsValid ?? false)
            {
                Console.WriteLine("☑️ The card number PASSES validation.");
            }
            else
            {
                Console.WriteLine("🚫 The card number FAILS validation.");
            }
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
        }
    }
}