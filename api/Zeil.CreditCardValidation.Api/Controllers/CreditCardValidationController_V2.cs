using Microsoft.AspNetCore.Mvc;
using Zeil.CreditCardValidation.Api.Models;
using Zeil.CreditCardValidation.Api.Services.Interfaces;

namespace Zeil.CreditCardValidation.Api.V2;


[ApiVersion("2.0")]
public class CreditCardValidationController(ILuhnValidationService creditCardValidationService) : BaseApiController
{
    /// <summary>
    /// Takes a card number and verifies it is a valid credit card number.      
    /// </summary>
    /// <response code="200">Card number validated</response>
    /// <response code="400">Card number is invalid</response>
    /// <response code="500">There was an unspecified problem.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes("application/json")]
    [Produces(typeof(CreditCardValidationResponseModel))]
    public async Task<IActionResult> Index(CreditCardValidationRequestModel requestModel)
    {
        var responseModel = new CreditCardValidationResponseModel
        {
            IsValid = await creditCardValidationService.IsValid(requestModel.CardNumber)
        };
        return new JsonResult(responseModel);
    }
}