using Microsoft.AspNetCore.Mvc;
namespace BuberBreakfast.Controller;

public class ErrorController : ControllerBase {
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}