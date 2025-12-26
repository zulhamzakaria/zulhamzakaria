using InterviewSystem.API.ErrorHandling;

namespace InterviewSystem.API.Middlewares;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
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

            var mapping = ExceptionMappings.Mappings
                .FirstOrDefault(x => x.ExceptionType.IsAssignableFrom(ex.GetType()));

            var statusCode = mapping?.StatusCode ?? StatusCodes.Status500InternalServerError;

            var code = mapping?.Code ?? "INTERNAL_SERVER_ERROR";

            var message = mapping?.Message ?? "An unexpected error occured";

            _logger.LogError(ex, "UnhandledException");
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                Code = code,
                Message = ex.InnerException
                //Message = message
            });

        }
    }

}


