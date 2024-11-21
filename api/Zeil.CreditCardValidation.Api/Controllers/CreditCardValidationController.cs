using Microsoft.AspNetCore.Mvc;
using Zeil.CreditCardValidation.Api.Services.Interfaces;

namespace Zeil.CreditCardValidation.Api;


[ApiVersion("1.0")]
public class CreditCardValidationController(ICreditCardValidationService creditCardValidationService) : BaseApiController
{
    /// <summary>
    /// Takes a card number and verifies it is a valid credit card number.      
    /// </summary>
    /// <response code="200">Card number validated</response>
    /// <response code="400">Card number is invalid</response>
    /// <response code="500">There was an unspecified problem.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    public async Task<IActionResult> Index(string cardNumber)
    {
        if (await creditCardValidationService.IsValid(cardNumber))
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }
}