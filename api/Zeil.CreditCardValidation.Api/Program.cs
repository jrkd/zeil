using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Zeil.CreditCardValidation.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICreditCardValidationService, CreditCardValidationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerDoc(description.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = $"Credit card validation api {description.ApiVersion}",
            Version = description.ApiVersion.ToString(),
            Description = $"An api to verify a credit card number is valid with the use of the [Luhn algorithm](https://en.wikipedia.org/wiki/Luhn_algorithm)",
            Contact = new() { Url = new Uri("https://zeil.com/"), Name = "Zeil" },
        });
        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(2, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
    }
});

if (app.Environment.IsDevelopment())
{
    // Save the Swagger JSON to a local file
    var swaggerProvider = app.Services.GetRequiredService<IApiDescriptionGroupCollectionProvider>();
    var swaggerGen = app.Services.GetRequiredService<Swashbuckle.AspNetCore.SwaggerGen.ISwaggerProvider>();
    var swagger = swaggerGen.GetSwagger("v1");

    var outputPath = Path.Combine(AppContext.BaseDirectory, "swagger.json");
    File.WriteAllText(outputPath, Newtonsoft.Json.JsonConvert.SerializeObject(swagger));
    Console.WriteLine($"Swagger JSON generated at: {outputPath}");
}
app.UseHttpsRedirection();

app.MapControllers().WithOpenApi();

app.Run();