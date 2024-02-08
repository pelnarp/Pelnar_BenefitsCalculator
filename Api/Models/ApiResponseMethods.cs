namespace Api.Models;

public partial class ApiResponse<T>
{
    public static ApiResponse<T> SuccessResponse(T result)
    {
        return new ApiResponse<T> { Success = true, Data = result };
    }

    public static ApiResponse<T> NotFoundResponse()
    {
        return new ApiResponse<T> { Success = false, Message = "Not found" };
    }

    public ApiResponse<T> ErrorResponse(string errorMessage)
    {
        return new ApiResponse<T> { Success = false, Error = errorMessage };
    }

    public ApiResponse<T> ErrorResponse(Exception exception)
    {
        return new ApiResponse<T> { Success = false, Error = exception.ToString() };
    }
}