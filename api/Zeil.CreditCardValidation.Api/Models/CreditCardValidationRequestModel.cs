namespace Zeil.CreditCardValidation.Api.Models;

/// <summary>
/// The credit card number model
/// </summary>
public class CreditCardValidationRequestModel
{
    /// <summary> The credit card number - this should be included without any formatting or spaces. </summary> 
    public string CardNumber { get; set; } = string.Empty;
}