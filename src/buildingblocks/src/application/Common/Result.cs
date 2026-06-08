using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Application.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public string Message { get; }
    public List<string> Errors { get; }
    public T Data { get; }

    private Result(bool isSuccess, T data, string message, List<string> errors)
    {
        IsSuccess = isSuccess;
        Data = data;
        Message = message;
        Errors = errors ?? new();
    }

    public static Result<T> Success(T data, string message = null) =>
        new(true, data, message, null);

    public static Result<T> Failure(string error) =>
        new(false, default, null, new List<string> { error });

    public static Result<T> Failure(List<string> errors) =>
        new(false, default, null, errors);
}