
namespace Zeil.CreditCardValidation.Api.Client;

public class CreditCardValidationClient : ICreditCardValidationClient
{
    public CreditCardValidationClient(System.Net.Http.HttpClient httpClient)
    {

    }
    public async Task<bool> Validate(string? cardNumber)
    {
        return false;
    }
}
