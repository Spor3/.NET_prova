using Microsoft.AspNetCore.Mvc;
using ErrorOr;
namespace BuberBreakfast.Controller;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase {
    [Route("/error")]
    protected IActionResult Problem(List<Error> errors ) 
    {
        var firstError = errors[0];
        var statusCode = firstError.Type switch 
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict =>  StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }

}