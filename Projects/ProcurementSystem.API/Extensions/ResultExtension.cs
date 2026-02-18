using Microsoft.AspNetCore.Mvc;
using ProcurementSystem.API.SharedKernel.Enums;
using ProcurementSystem.API.SharedKernel.ErrorHandling;

namespace ProcurementSystem.API.Extensions;

public static class ResultExtension
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result);

        var errorType = result.Errors[0].ErrorType;
        var statusCode = MapStatusCode(errorType);

        if (errorType == ErrorType.Validation)
            return ToValidateProblem(result.Errors);

        return ToProblem(result.Errors, statusCode);


    }

    private static IActionResult ToProblem(IReadOnlyList<Error> errors, int statusCode)
    {
        var problems = new ProblemDetails
        {
            Title = "Request Failed",
            Status = statusCode,
            Detail = string.Join("; ", errors.Select(errors => errors.Message))
        };

        problems.Extensions["errors"] = errors;

        return new ObjectResult(problems)
        {
            StatusCode = statusCode,
        };

    }

    private static IActionResult ToValidateProblem(IReadOnlyList<Error> errors)
    {
        var grouped = errors
            .GroupBy(e => e.ErrorType.ToString() ?? string.Empty)
            .ToDictionary
            (g => g.Key,
            g => g.Select(e => e.Message).ToArray());

        var problems = new ValidationProblemDetails(grouped)
        {
            Title = "Validation Failed",
            Status = StatusCodes.Status400BadRequest,
        };

        return new BadRequestObjectResult(problems);
    }

    private static int MapStatusCode(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.BusinessRule => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
