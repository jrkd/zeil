namespace Zeil.CreditCardValidation.Api.Client;

public interface ICreditCardValidationClient
{
    Task<bool> Validate(string? cardNumber);
}
