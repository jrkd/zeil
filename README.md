# Zeil - Credit card validation API 
This code provides an API service for a client to verify a credit card number is valid with the use of the [Luhn algorithm](https://en.wikipedia.org/wiki/Luhn_algorithm). 

See [The OpenAPI html spec](/todo-openapi-url) for how to format a request, and responses to expect

See below for prerequisites and setup instructions. 

## Prerequisites
To compile the code you need [dotnet 8 sdk](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed

(If you're using docker locally, make sure you have [docker installed](https://docs.docker.com/get-started/get-docker/)

## Running locally 
Create an `/api/Zeil.CreditCardValidation.Api/.env` file that includes a secret key. This shouldnt be committed to the codebase.
`SECRET_KEY = <...>`
`PORT = <...>`

Navigate to the Api folder 
`cd ./api/Zeil.CreditCardValidation.Api`

Run the api 
`dotnet watch run`

Api by default should be configured be accessed via `https://localhost:5043` - this should redirect to the OpenApi html spec. 

### Tests 
To run the unit tests

Navigate to the tests project folder
`cd ./api/Zeil.CreditCardValidation.Api.Tests`

run the tests from commandline 
`dotnet test`

### Example client uses 
To step through example C# client use 

Navigate to the sandbox project folder 
`cd ./sandbox/Zeil.CreditCardValidation.Sandbox`

run the console project 
`dotnet run`

Input the desired credit card number in the console when prompted. 

## Deploying 

Use `dotnet publish` output, an external build process, or the `./api/Dockerfile` to deploy to infrastructure. 

## Deployment security 
 
We recommend the API is deployed into secure infrastructure environment with private network access. If hosted on a public domain, we've included Rate limiting settings in C#, but suggest this is managed as part of the infrastructure before a request reaches the API. 

Also, as we are dealing with credit card details, ensure the service can only be accessed via SSL. 