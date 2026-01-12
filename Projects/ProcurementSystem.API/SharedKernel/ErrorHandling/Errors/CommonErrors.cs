using ProcurementSystem.API.SharedKernel.Enums;

namespace ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;

public static class CommonErrors
{
    public static Error NotFound(string entityName, string identifier)
        => new Error(
            errorCode: "COMMON_NOT_FOUND",
            message: $"{entityName} with identifier '{identifier}' was not found.",
            errorType: ErrorType.NotFound);
    public static Error InvalidInput(string details)
        => new Error(
            errorCode: "COMMON_INVALID_INPUT",
            message: $"Invalid input: {details}",
            errorType: ErrorType.Validation);
    public static Error UnauthorizedAccess(string action)
            => new Error(
                errorCode: "COMMON_UNAUTHORIZED",
                message: $"Unauthorized access to perform action: {action}",
                errorType: ErrorType.Unauthorized);
    public static Error NegativeAmount()
        => new Error(
            errorCode: "COMMON_NEGATIVE_AMOUNT",
            message: "Amount cannot be negative.",
            errorType: ErrorType.Validation);
}
