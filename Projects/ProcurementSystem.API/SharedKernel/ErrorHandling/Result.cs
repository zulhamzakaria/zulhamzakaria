namespace ProcurementSystem.API.SharedKernel.ErrorHandling;


public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public IReadOnlyList<Error> Errors { get; }
    protected Result(bool isSuccess, IReadOnlyList<Error> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }
    public static Result Success() 
        => new Result(true, Array.Empty<Error>());
    public static Result Failure(Error error)
    {
        if(error is null)
            throw new ArgumentNullException("There must be at least one error.", nameof(error));
        return new Result(false, [error]);
    }
    public static Result Failure(IReadOnlyList<Error> errors)
    {
        if(errors is null || errors.Count == 0)
            throw new ArgumentNullException("There must be at least one error.", nameof(errors));
        return new Result(false, errors.ToList());
    }
}
public sealed class Result<T> : Result
{
    public T? Value { get; }

    public Result(T? value) : base(true, Array.Empty<Error>())
    {
        Value = value;
    }

    private Result(IReadOnlyList<Error> errors) : base(false, errors)
    {
        Value = default;
    }

    public static Result<T> Success(T value) 
        => new Result<T>(value);

    public static new Result<T> Failure(Error error)
    {
        if(error is null)
            throw new ArgumentNullException("There must be at least one error.", nameof(error));
        return new Result<T>([error]);
    }

    public static new Result<T> Failure(IReadOnlyList<Error> errors)
    {
        if(errors is null || errors.Count == 0)
            throw new ArgumentNullException("There must be at least one error.", nameof(errors));
        return new (errors);
    }

}
