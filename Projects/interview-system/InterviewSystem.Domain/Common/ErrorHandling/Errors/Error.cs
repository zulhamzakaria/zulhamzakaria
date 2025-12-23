using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Domain.Common.ErrorHandling.Errors;

public sealed record Error
{
    public string Code { get; } = string.Empty;
    public string Message { get; } = string.Empty;
    public ErrorType ErrorType { get; }

    public Error(ErrorType errorType, string code, string message)
    {
        ErrorType = errorType;
        Code = code;
        Message = message;
    }
}
