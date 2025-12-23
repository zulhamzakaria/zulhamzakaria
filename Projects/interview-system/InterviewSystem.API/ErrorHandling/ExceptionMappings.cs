using Microsoft.EntityFrameworkCore;

namespace InterviewSystem.API.ErrorHandling;

internal static class ExceptionMappings
{
    public static readonly IReadOnlyList<ExceptionMapping> Mappings =
    [
            new(typeof(UnauthorizedAccessException),
                StatusCodes.Status401Unauthorized,
                "UNAUTHORIZED",
                "Unauthorized access"),

            new(typeof(ArgumentException),
                StatusCodes.Status400BadRequest,
                "INVALID_ARGUMENT",
                "Invalid argument provided"),

            new(
                typeof(InvalidOperationException),
                StatusCodes.Status409Conflict,
                "INVALID_OPERATION",
                "The operation is not valid in the current state"),

            new(
                typeof(DbUpdateException),
                StatusCodes.Status503ServiceUnavailable,
                "DATABASE_ERROR",
                "A database error occurred"
            )
    ];
}
