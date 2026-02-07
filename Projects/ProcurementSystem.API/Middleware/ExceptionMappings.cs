using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.SharedKernel.Enums;
using ProcurementSystem.API.SharedKernel.ErrorHandling;

namespace ProcurementSystem.API.Middleware;

public static class ExceptionMappings
{
    private static readonly Dictionary<Type, ExceptionMapping> _mappings = new()
    {
        {
            typeof(ArgumentNullException),
            new ExceptionMapping(
                StatusCodes.Status400BadRequest,
                new Error("ARGUMENT_NULL", "A required argument was null.", ErrorType.Exception)
            )
        },
        {
            typeof(UnauthorizedAccessException),
            new ExceptionMapping(
                StatusCodes.Status401Unauthorized,
                new Error("UNAUTHORIZED", "Access is denied due to invalid credentials.", ErrorType.Exception)
            )
        },
        {
            typeof(KeyNotFoundException),
            new ExceptionMapping(
                StatusCodes.Status404NotFound,
                new Error("NOT_FOUND", "The requested resource was not found.", ErrorType.Exception)
            )
        },
        {
            typeof(DbUpdateException),
            new ExceptionMapping
            (StatusCodes.Status503ServiceUnavailable,
            new Error("DATABASE_ERROR", "A database error has occurred", ErrorType.Exception))
        }
    };

    public static ExceptionMapping? GetMapping(Exception ex)
    {
        var exceptionType = ex.GetType();
       
        foreach (var (type, mapping) in _mappings)
        {
            if (type.IsAssignableFrom(exceptionType))
            {
                return mapping;
            }
        }

        return null;
    }
}

public record ExceptionMapping(int StatusCode, Error Error);
