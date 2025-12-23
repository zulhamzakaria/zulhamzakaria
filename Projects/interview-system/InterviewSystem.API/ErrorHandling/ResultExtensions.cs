using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using Microsoft.AspNetCore.Mvc;

namespace InterviewSystem.API.ErrorHandling;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        return result.Errors.FirstOrDefault()?.ErrorType switch
        {
            ErrorType.Validation => new BadRequestObjectResult(result.Errors),
            ErrorType.BusinessRule => new ConflictObjectResult(result.Errors),
            _ => new ObjectResult(result.Errors) { StatusCode = StatusCodes.Status500InternalServerError }
        };

    }
}
