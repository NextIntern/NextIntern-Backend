using Microsoft.AspNetCore.Mvc.Filters;
using SWD.NextIntern.Service.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FluentValidation;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var traceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;

            switch (context.Exception)
            {
                case ValidationException validationException:
                    HandleValidationException(context, validationException, traceId);
                    break;
                case ForbiddenAccessException forbiddenAccessException:
                    HandleForbiddenAccessException(context, forbiddenAccessException, traceId);
                    break;
                case UnauthorizedAccessException unauthorizedAccessException:
                    HandleUnauthorizedAccessException(context, unauthorizedAccessException, traceId);
                    break;
                default:
                    HandleUnknownException(context, traceId);
                    break;
            }
        }

        private void HandleValidationException(ExceptionContext context, ValidationException exception, string traceId)
        {
            foreach (var error in exception.Errors)
            {
                context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            var errors = new ValidationProblemDetails(context.ModelState);
            var response = new ResponseObject<ValidationProblemDetails>(errors, System.Net.HttpStatusCode.InternalServerError, "Validation errors occurred.");

            context.Result = new BadRequestObjectResult(response);
            context.ExceptionHandled = true;
        }

        private void HandleForbiddenAccessException(ExceptionContext context, ForbiddenAccessException exception, string traceId)
        {
            var response = new ResponseObject<string>(System.Net.HttpStatusCode.Forbidden, "You do not have permission to perform this action.");

            context.Result = new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
            context.ExceptionHandled = true;
        }

        private void HandleUnauthorizedAccessException(ExceptionContext context, UnauthorizedAccessException exception, string traceId)
        {
            var response = new ResponseObject<string>(System.Net.HttpStatusCode.Unauthorized, "You are not authorized to perform this action.");

            context.Result = new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
            context.ExceptionHandled = true;
        }

        private void HandleUnknownException(ExceptionContext context, string traceId)
        {
            var response = new ResponseObject<string>(System.Net.HttpStatusCode.InternalServerError, "An unexpected error occurred. Please try again later.");

            context.Result = new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.ExceptionHandled = true;
        }
    }
}