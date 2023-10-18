namespace PlannerApp.Shared.Responses;
// handles reponses for API requests
public class ApiResponseBase
{
    public string? Message { get; set; }
    public bool IsSuccess { get; set; }
}

public class ApiResponse<T> : ApiResponseBase
{
    public T? Value { get; set; }
}
