namespace RoyalVilla_API.dtos;

public class ApiResponse<TData>
{
    public bool Success { get; set; }

    public int StatusCode { get; set; }

    public string Message { get; set; } = string.Empty;

    public TData? Data { get; set; }

    public Object? Errors { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
   

    public static ApiResponse<TData> SendErrorResponse(int statusCode, string message, object? err=null)
    {
        
        return new ApiResponse<TData>
        {

            Success = false,
            StatusCode = statusCode,
            Message = message,
            Data = default,
            Errors = err,
            
        };


    }

    public static ApiResponse<TData> SendSuccessResponse(TData data, string message = "")
    {
        return new ApiResponse<TData>
        {
            Success = true,
            StatusCode = 200,
            Message = message,
            Data = data,
            Errors = null,
            TimeStamp = DateTime.UtcNow
        };
    }

}

