namespace Zeil.CreditCardValidation.Api;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class BaseApiController : ControllerBase
{

}