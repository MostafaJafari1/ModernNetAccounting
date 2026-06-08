namespace BuildingBlocks.Application.Common;

public sealed class Result<T>
{
    private readonly T? _value;

    private Result(T? value, bool isSuccess, IReadOnlyList<Error> errors)
    {
        if (isSuccess && errors.Any(e => e != Error.None))
            throw new InvalidOperationException("A successful result cannot contain errors.");

        if (!isSuccess && errors.Count == 0)
            throw new InvalidOperationException("A failed result must contain at least one error.");

        _value = value;
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("A failed result does not have a value.");

    public Error Error => Errors.FirstOrDefault() ?? Error.None;
    public IReadOnlyList<Error> Errors { get; }

    //Factory Methods

    public static Result<T> Success(T value)
        => new(value, true, [Error.None]);

    public static Result<T> Failure(Error error)
        => new(default, false, [error]);

    public static Result<T> Failure(IReadOnlyList<Error> errors)
        => new(default, false, errors);

    //Implicit Operators

    public static implicit operator Result<T>(T value)
        => Success(value);

    public static implicit operator Result<T>(Error error)
        => Failure(error);

    public static implicit operator Result<T>(List<Error> errors)
        => Failure(errors);
}