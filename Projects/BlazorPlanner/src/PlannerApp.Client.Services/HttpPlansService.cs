using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System.Net.Http.Json;

namespace PlannerApp.Client.Services;

public class HttpPlansService : IPlansService
{
    private readonly HttpClient _httpClient;
    public HttpPlansService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<ApiResponse<PagedList<PlanSummary>>> GetPlansAsync(string? query = null, int pageNo = 1, int pageSize = 10)
    {
        var response = await _httpClient.GetAsync($"/api/v2/plans?query={query}&pageNumber={pageNo}&pageSize={pageSize}");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedList<PlanSummary>>>();
            return result!;
        }

        var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
        throw new ApiException(errorResponse!, response.StatusCode);
    }
}
