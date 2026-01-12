namespace ProcurementSystem.API.SharedKernel.ErrorHandling;

public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public IReadOnlyList<Error> Errors { get; }

    public Result(bool isSuccess, T? value, IReadOnlyList<Error> errors)
    {
        IsSuccess = isSuccess;
        Value = value;
        Errors = errors;
    }

    public static Result<T> Success(T value) 
        => new Result<T>(true, value, Array.Empty<Error>());

    public static Result<T> Failure(IReadOnlyList<Error> errors)
    {
        if(errors.Any() is false)
            throw new ArgumentNullException("There must be at least one error for a failure result.", nameof(errors));

        return new Result<T>(false, default, errors.ToList());
    }

}
