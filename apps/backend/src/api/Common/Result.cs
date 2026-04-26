namespace api.Common;

public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    public int StatusCode { get; }

    protected Result(bool isSuccess, string? error, int statusCode)
    {
        IsSuccess = isSuccess;
        Error = error;
        StatusCode = statusCode;
    }

    public static Result Success()
    {
        return new Result(true, null, 200);
    }

    public static Result Failure(string error, int statusCode = 400)
    {
        return new Result(false, error, statusCode);
    }

    public static Result NotFound(string error = "Resource not found")
    {
        return new Result(false, error, 404);
    }

    public static Result BadRequest(string error = "Bad request")
    {
        return new Result(false, error, 400);
    }

    public static Result Conflict(string error = "Conflict")
    {
        return new Result(false, error, 409);
    }
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(bool isSuccess, T? value, string? error, int statusCode)
        : base(isSuccess, error, statusCode)
    {
        Value = value;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, null, 200);
    }

    public static Result<T> Created(T value)
    {
        return new Result<T>(true, value, null, 201);
    }

    public static new Result<T> Failure(string error, int statusCode = 400)
    {
        return new Result<T>(false, default, error, statusCode);
    }

    public static new Result<T> NotFound(string error = "Resource not found")
    {
        return new Result<T>(false, default, error, 404);
    }

    public static new Result<T> BadRequest(string error = "Bad request")
    {
        return new Result<T>(false, default, error, 400);
    }

    public static new Result<T> Conflict(string error = "Conflict")
    {
        return new Result<T>(false, default, error, 409);
    }
}
