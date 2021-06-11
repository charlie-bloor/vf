using System;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Musicalog.Data.Exceptions;

namespace Musicalog.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private const string ContentType = "application/json";
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next,
                                           ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(httpContext, ex);
            }
            catch (EntityNotFoundException ex)
            {
                await HandleEntityNotFoundExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleValidationExceptionAsync(HttpContext context, ValidationException validationException)
        {
            var validationErrors =
                validationException.Errors
                                   .GroupBy(failure => failure.PropertyName)
                                   .ToDictionary(group => group.Key, group => group.Select(x => x.ErrorMessage).ToArray());

            var problemDetails = new ValidationProblemDetails(validationErrors)
            {
                Detail = validationException.Message,
                Status = 400,
                Title = "One or more validation errors occurred"
            };

            // TODO: Log as error

            var json = JsonSerializer.Serialize(problemDetails);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            return context.Response.WriteAsync(json);
        }

        private Task HandleEntityNotFoundExceptionAsync(HttpContext context, EntityNotFoundException entityNotFoundException)
        {
            var statusCode = HttpStatusCode.NotFound;
            context.Response.ContentType = ContentType;
            context.Response.StatusCode = (int)statusCode;

            var problemDetails = new ProblemDetails
            {
                Detail = entityNotFoundException.Message,
                Status = context.Response.StatusCode,
                Title = "Not Found",
                Type = statusCode.ToString()
            };

            // TODO: Log as warning

            var json = JsonSerializer.Serialize(problemDetails);
            return context.Response.WriteAsync(json);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = ContentType;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var problemDetails = new ProblemDetails
            {
                Detail = exception.Message,
                Status = (int)HttpStatusCode.InternalServerError,
                Title = exception.GetType().ToString()
            };

            // TODO: Log as error

            var json = JsonSerializer.Serialize(problemDetails);
            return context.Response.WriteAsync(json);
        }
    }
}