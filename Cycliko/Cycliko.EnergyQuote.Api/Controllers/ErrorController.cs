using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Cycliko.EnergyQuote.Api.Controllers
{
    public class ErrorController : ControllerBase
    {
        [Route("/error-development")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return Problem(
                detail: exceptionHandlerFeature?.Error.StackTrace,
                title: exceptionHandlerFeature?.Error.Message ?? "An error occured"
            );
        }

        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return Problem(title: exceptionHandlerFeature?.Error.Message ?? "An error occured");
        }
    }
}
