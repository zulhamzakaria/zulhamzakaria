using ProcurementSystem.API.SharedKernel.Enums;

namespace ProcurementSystem.API.SharedKernel.ErrorHandling;

public sealed class Error
{
    public string ErrorCode { get; }
    public string Message { get; }
    public ErrorType ErrorType { get; set; }

    public Error(string errorCode, string message, ErrorType errorType)
    {
        ErrorCode = errorCode;
        Message = message;
        ErrorType = errorType;
    }

}
