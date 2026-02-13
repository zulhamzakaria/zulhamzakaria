using System.Security.Claims;

namespace ProcurementSystem.API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionMiddleware(RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
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

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        if(context.Response.HasStarted)
        {
            _logger.LogError("The response has already started, the global exception middleware will not be executed.");
            throw ex;
        }

        var mapping = ExceptionMappings.GetMapping(ex);

        var statusCode = mapping?.StatusCode 
            ?? StatusCodes.Status500InternalServerError;
        var code = mapping?.Error.ErrorCode
            ?? "Internal_Server_Error";
        var message = _env.IsDevelopment() && mapping is null 
            ? ex.Message
            : mapping?.Error.Message 
            ?? "An unexpected error occurred. Please try again later.";
        var logLevel = mapping != null 
            ? LogLevel.Warning 
            : LogLevel.Error;
        var userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? context.User?.FindFirst("sub")?.Value
            ?? "Anonymous";
        _logger.Log
            (logLevel, 
            mapping == null ? ex : null,
            "Exception handled: {Code} | {Method} {Path} | User: {UserId} | TraceId: {TraceId}",
            code,
            context.Request.Method,
            context.Request.Path,
            userId,
            context.TraceIdentifier);

        context.Response.Clear();
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse
            (code,
            message,
            context.TraceIdentifier,
            _env.IsDevelopment() && mapping is null ? ex.ToString() : null);

        await context.Response.WriteAsJsonAsync(response);
    }
}

public sealed record ErrorResponse(
    string Code,
    string Message,
    string TraceId,
    string? Detail = null
);