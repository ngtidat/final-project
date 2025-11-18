using System.Net;
using System.Text.Json;
using Misa.CRM.Business.Common.Exceptions;

namespace Misa.CRM.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unhandled exception occurred");

        var statusCode = exception switch
        {
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            ResourceNotFoundException => HttpStatusCode.NotFound,
            ResourceUniqueException => HttpStatusCode.Conflict,
            DatabaseBadRequestException => HttpStatusCode.BadRequest,
            InvalidOperationException => HttpStatusCode.BadRequest,
            ArgumentException => HttpStatusCode.BadRequest,
            RequestValidationException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        var response = new
        {
            StatusCode = (int)statusCode,
            Message = exception.Message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var json = JsonSerializer.Serialize(response, options);
        await context.Response.WriteAsync(json);
    }
}
