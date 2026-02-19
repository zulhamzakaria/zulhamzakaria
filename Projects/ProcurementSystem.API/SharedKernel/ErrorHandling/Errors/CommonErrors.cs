using ProcurementSystem.API.SharedKernel.Enums;

namespace ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;

public static class CommonErrors
{
    public static Error NotFound(string entityName, string identifier)
        => new Error(
            errorCode: "COMMON_NOT_FOUND",
            message: $"{Helper.Humanize(entityName)} with identifier '{identifier}' was not found.",
            errorType: ErrorType.NotFound);
    public static Error NotFound(string entityName)
        => new Error(
            errorCode: "COMMON_NOT_FOUND",
            message: $"No {Helper.Humanize(entityName)}s found. Please add some first",
            errorType: ErrorType.NotFound);
    public static Error InvalidInput(string fieldName)
        => new Error(
            errorCode: "COMMON_INVALID_INPUT",
            message: $"Invalid input: {Helper.Humanize(fieldName)}",
            errorType: ErrorType.Validation);
    public static Error InvalidLength(string fieldName, int minLength, int maxLength)
       => new Error(
           errorCode: "COMMON_INVALID_Length",
           message: $"{Helper.Humanize(fieldName)} length must be between {minLength} and {maxLength}",
           errorType: ErrorType.Validation);
    public static Error Required(string fieldName)
        => new Error(
            errorCode: "COMMON_REQUIRED_FIELD",
            message: $"{Helper.Humanize(fieldName)} is required.",
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
    public static Error DuplicateEntry(string entityName, string identifier)
        => new Error(
            errorCode: "COMMON_DUPLICATE_ENTRY",
            message: $"{Helper.Humanize(entityName)} with identifier '{identifier}' already exists.",
            errorType: ErrorType.Validation);
    public static Error InvalidEnumValue(string fieldName, string invalidValue)
        => new Error(
            errorCode: "COMMON_INVALID_ENUM_VALUE",
            message: $"Invalid value '{invalidValue}' for {Helper.Humanize(fieldName)}.",
            errorType: ErrorType.Validation);
    public static Error InvalidStatusChange(string status)
        => new Error(
            errorCode: "COMMON_INVALID_STATUS_CHANGE",
            message: $"Status is already '{status}'.",
            errorType: ErrorType.Validation);
}
