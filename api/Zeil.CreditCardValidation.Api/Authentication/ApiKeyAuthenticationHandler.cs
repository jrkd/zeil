using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Zeil.CreditCardValidation.Api;

///<summary> Authentication handler class for API calls to ensure valid api key is used.</summary>
public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private const string ApiKeyHeaderName = "X-Api-Key";

    private readonly ApiAuthenticationOptions apiCredentialsOptions;

    [Obsolete]
    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IOptions<ApiAuthenticationOptions> apiCredentialsOptions
        )
        : base(options, logger, encoder, clock)
    {
        this.apiCredentialsOptions = apiCredentialsOptions.Value;
    }

    ///<summary> Authentication handler for API calls to ensure valid api key is used.</summary>
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValue))
        {
            return Task.FromResult(AuthenticateResult.Fail("API Key header not found."));
        }

        if (!string.Equals(apiKeyHeaderValue, apiCredentialsOptions.ApiKey, StringComparison.Ordinal))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API Key."));
        }

        // Create the ClaimsPrincipal
        var claims = new[] { new Claim(ClaimTypes.Name, "API User") };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}