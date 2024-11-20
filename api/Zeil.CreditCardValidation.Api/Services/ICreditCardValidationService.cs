namespace Zeil.CreditCardValidation.Api.Services.Interfaces;

public interface ICreditCardValidationService 
{
    public Task<bool> IsValid(string cardNumber);
}