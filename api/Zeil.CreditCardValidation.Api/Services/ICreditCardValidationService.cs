namespace Zeil.CreditCardValidation.Api.Services.Interfaces;

public interface ILuhnValidationService
{
    public Task<bool> IsValid(string? cardNumber);
}