namespace Zeil.CreditCardValidation.Api;

/// <summary>
/// Options class to retrieve the ApiKey to allow access to the API.
/// </summary>
public class ApiAuthenticationOptions
{
    /// <summary>
    /// API key securing api.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;
}