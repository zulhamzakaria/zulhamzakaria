using InterviewSystem.Domain.Common.ErrorHandling.Errors;

namespace InterviewSystem.Domain.Common.ErrorHandling;

public class Result<T>
{
    public bool IsSuccess { get;}
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public string? ErrorMessage { get; }
    public string? ErrorCode { get; }

    private Result(bool isSuccess, T? value, string errorMessage, string? errorCode)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
    }

    public static Result<T> Success(T value)
        => new Result<T>(true, value, string.Empty, null);

    //public static Result<T> Failure(string errorMessage, string errorCode)
    //{
    //    if (string.IsNullOrWhiteSpace(errorCode))
    //        throw new ArgumentNullException("Error code must be provided!");

    //    return new Result<T>(false, default, errorMessage, errorCode);
    //}

    public static Result<T> Failure(Error error)
    {
        if (string.IsNullOrWhiteSpace(error.Code))
            throw new ArgumentNullException("Error code must be provided!");
        return new Result<T>(false, default, error.Message, error.Code);
    }

}
