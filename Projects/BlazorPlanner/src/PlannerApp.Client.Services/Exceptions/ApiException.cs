using PlannerApp.Shared.Responses;
using System.Net;

namespace PlannerApp.Client.Services.Exceptions;

public class ApiException : Exception
{
    public ApiErrorResponse? ApiErrorResponse { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public ApiException(ApiErrorResponse apiErrorResponse, HttpStatusCode statusCode) : this(apiErrorResponse)
    {
        StatusCode = statusCode;
    }

    public ApiException(ApiErrorResponse apiErrorResponse)
    {
        ApiErrorResponse = apiErrorResponse;
    }
}
