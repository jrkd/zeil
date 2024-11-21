
namespace Zeil.CreditCardValidation.Api.Services.Interfaces;

public class CreditCardValidationService : ICreditCardValidationService
{
    public async Task<bool> IsValid(string? cardNumber)
    {
        return false;
    }
}