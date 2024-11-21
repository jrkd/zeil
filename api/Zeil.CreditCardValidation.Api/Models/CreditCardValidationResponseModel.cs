namespace Zeil.CreditCardValidation.Api.Models;

/// <summary>
/// The credit card number model
/// </summary>
public class CreditCardValidationResponseModel
{
    /// <summary> Whether the request passed validation result</summary> 
    public bool IsValid { get; set; }
}