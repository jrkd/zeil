# Zeil - Credit card validation API 
This code provides an API service for a client to verify a credit card number is valid with the use of the [Luhn algorithm](https://en.wikipedia.org/wiki/Luhn_algorithm). 

See [The OpenAPI html spec](/swagger) for how to format a request, and responses to expect. 

See below for prerequisites and setup instructions. 

## Prerequisites
To compile the code you need [dotnet 8 sdk](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed

## Running locally 
Add an Api key to `api\Zeil.CreditCardValidation.Api\appsettings.json` 
This shouldnt be committed to the codebase.
```json
{
  "ApiAuthenticationOptions": {
    "ApiKey": "<Key your going to be accessing the api with>"
  }
}
```

Navigate to the Api folder 
`cd ./api/Zeil.CreditCardValidation.Api`

Run the api 
`dotnet watch run`

Api by default should be configured be accessed via `http://localhost:5272/swagger` - this should redirect to the OpenApi html spec. 

The 

### Tests 
To run the unit tests, navigate to the tests project folder
`cd ./api/Zeil.CreditCardValidation.Api.Tests`

run the tests from command line `dotnet test` or IDE 

### Example client uses 
To step through example C# client use, navigate to the sandbox project folder 
`cd ./sandbox/Zeil.CreditCardValidation.Sandbox`

Update the sandbox `appsettings.json` to have a matching api key;
```json
{
    "CreditCardValidationOptions": {
        "BaseAddress": "http://localhost:5272",
        "ApiKey": "<Key your going to be accessing the api with>"
    }
}
```

These can be anything, they just need to match each other.

Then also make sure that either you also running the `Zeil.CreditCardValidation.Api` locally, and that the `CreditCardValidationOptions.BaseAddress` in `sandbox\Zeil.CreditCardValidation.Sandbox\appsettings.json` matches it. 

Or point that BaseAddress to where the api is hosted, and ensure your able to access it.

run the console project 
`dotnet run`

Input the desired credit card number in the console when prompted. This will make a request via the `/generated/Client.cs` to the Api

*NB: The sandbox is currently configured to use v2 of the Api.*

## Deployment security 
 
We recommend the API is deployed into secure infrastructure environment with private network access. If hosted on a public domain, we suggest standard security measures - rate limiting, ip white listing & secure storage of the API is managed as part of the infrastructure before a request reaches the API. 

Also, as we are dealing with credit card details, ensure the service can only be accessed via SSL. 