namespace ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;

public static class CommonErrors
{
    public static Error NotFound(string entityName, string identifier)
        => new Error(
            errorCode: "COMMON_NOT_FOUND",
            message: $"{entityName} with identifier '{identifier}' was not found.",
            errorType: Enums.ErrorType.NotFound);
    public static Error InvalidInput(string details)
        => new Error(
            errorCode: "COMMON_INVALID_INPUT",
            message: $"Invalid input: {details}",
            errorType: Enums.ErrorType.Validation);
}
