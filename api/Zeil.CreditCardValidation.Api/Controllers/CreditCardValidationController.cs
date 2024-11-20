using Microsoft.AspNetCore.Mvc;
using Zeil.CreditCardValidation.Api.Services.Interfaces;

namespace Zeil.CreditCardValidation.Api;


public class CreditCardValidationController(ICreditCardValidationService creditCardValidationService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        string number = "";
        if (await creditCardValidationService.IsValid(number))
        {
            return Ok();
        }
        else
        {
            return ValidationProblem();
        }
    }
}