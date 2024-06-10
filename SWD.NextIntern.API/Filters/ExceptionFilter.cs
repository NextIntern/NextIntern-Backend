using Microsoft.AspNetCore.Mvc.Filters;
using SWD.NextIntern.Service.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SWD.NextIntern.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ForbiddenAccessException:
                    context.Result = new ForbidResult();
                    context.ExceptionHandled = true;
                    break;
                case UnauthorizedAccessException:
                    context.Result = new ForbidResult();
                    context.ExceptionHandled = true;
                    break;
            }
        }
    }
    internal static class ProblemDetailsExtensions
    {
        public static IActionResult AddContextInformation(this ObjectResult objectResult, ExceptionContext context)
        {
            if (objectResult.Value is not ProblemDetails problemDetails)
            {
                return objectResult;
            }
            problemDetails.Extensions.Add("traceId", Activity.Current?.Id ?? context.HttpContext.TraceIdentifier);

            return objectResult;
        }
    }
}
