namespace InterviewSystem.API.ErrorHandling;

internal sealed record ExceptionMapping(
    Type ExceptionType, 
    int StatusCode, 
    string Code, 
    string Message);

