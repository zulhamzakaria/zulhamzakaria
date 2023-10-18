using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System.Net.Http.Json;

namespace PlannerApp.Client.Services;

public class HttpAuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    public HttpAuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    async Task<ApiResponse<ApiErrorResponse>> IAuthenticationService.RegisterUserAsync(RegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/v2/auth/register", request);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ApiErrorResponse>>();
            return result!;
        }

        var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
        throw new ApiException(errorResponse!, response.StatusCode);

    }
}
