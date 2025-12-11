namespace InterviewSystem.Domain.Common.ErrorHandling.Errors;

public sealed record Error
{
    public string Code { get; } = string.Empty;
    public string Message { get; } = string.Empty;

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
}
