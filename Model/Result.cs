namespace BlockchainNet.Model;

public class Result<T>
{
    public bool Success { get; private set; }
    public int StatusCode { get; private set; }
    public T Data { get; private set; }
    public string Message { get; private set; }
    public List<string> Errors { get; private set; }

    private Result(bool success, T data, string message, List<string> errors, int statusCode)
    {
        Success = success;
        Data = data;
        Message = message;
        Errors = errors ?? new List<string>();
        StatusCode = statusCode;
    }
    
    public static Result<T> Ok(T data, string message = "Operation successful", int statusCode = 200)
        => new Result<T>(true, data, message, null, statusCode);

    public static Result<T> Fail(string message, int statusCode = 400, List<string> errors = null)
        => new Result<T>(false, default, message, errors, statusCode);

    public static Result<T> ValidationError(string message, List<string> validationErrors)
        => new Result<T>(false, default, message, validationErrors, 422);
}