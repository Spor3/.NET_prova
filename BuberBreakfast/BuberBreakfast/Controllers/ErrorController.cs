using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ErrorOr;
namespace BuberBreakfast.Controller;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase {
    [Route("/error")]
    protected IActionResult Problem(List<Error> errors ) 
    {
        if(errors.All(error => error.Type ==ErrorType.Validation ))
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach (var e in errors)
            {
                modelStateDictionary.AddModelError(e.Code, e.Description);
            }
            return ValidationProblem(modelStateDictionary);
        }
        if(errors.Any(e => e.Type == ErrorType.Unexpected))
        {
            return Problem();
        }
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