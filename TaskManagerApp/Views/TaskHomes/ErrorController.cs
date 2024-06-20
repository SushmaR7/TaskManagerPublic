using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerApp.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        // GET: ErrorController
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exceptionDetails?.Path;
            ViewBag.ExceptionMessage = exceptionDetails?.Error.Message;
            ViewBag.innerExp = exceptionDetails?.Error.InnerException;
            ViewBag.data = exceptionDetails?.Error.Data;
            return View();
        }

    }
}
