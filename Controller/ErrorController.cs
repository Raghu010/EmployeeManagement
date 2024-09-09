using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace EmployeeManagement.Controller
{
    public class ErrorController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILogger<ErrorController> logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }
        
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";

                    //ViewBag.Path = statusCodeResult.OriginalPath;
                    //ViewBag.QS = statusCodeResult.OriginalQueryString;

                    logger.LogWarning($"404 error occured. Path = {statusCodeResult.OriginalPath} " + $"and query string = {statusCodeResult.OriginalQueryString}");
                    break;

            }
            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //Note: Displaying the exception developer page to end user might pose a security risk.
            //ViewBag.ExceptionPath = exceptionDetails.Path;
            //ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            //ViewBag.StackTrace = exceptionDetails.Error.StackTrace;

            logger.LogError($"The Path {exceptionDetails.Path} threw an exception " + $"{exceptionDetails.Error}");

            return View("Error");
        }
    }
}
