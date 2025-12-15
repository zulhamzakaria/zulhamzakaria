using InterviewSystem.Domain.Common.ErrorHandling.Errors;

namespace InterviewSystem.Domain.Common.ErrorHandling;

public class Result<T>
{
    public bool IsSuccess { get;}
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public IReadOnlyList<Error> Errors { get; }

    private Result(bool isSuccess, T? value, IReadOnlyList<Error> errors)
    {
        IsSuccess = isSuccess;
        Value = value;
        Errors = errors;
    }

    public static Result<T> Success(T value)
        => new Result<T>(true, value, Array.Empty<Error>());

    public static Result<T> Failure(Error error)
    {
        if (string.IsNullOrWhiteSpace(error.Code))
            throw new ArgumentNullException("Error code must be provided!");
        return new Result<T>(false, default, [error]);
    }

    public static Result<T> Failure(IEnumerable<Error> errors)
    {
        if(errors == null || !errors.Any()) 
            throw new ArgumentNullException("At least one Error must be provided");
        return new Result<T>(false, default, errors.ToList());
    }

}
