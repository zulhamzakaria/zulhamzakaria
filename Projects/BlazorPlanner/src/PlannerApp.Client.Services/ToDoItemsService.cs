using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System.Net.Http.Json;
using System.Numerics;

namespace PlannerApp.Client.Services;

public class ToDoItemsService : IToDoItemsService
{
    private readonly HttpClient _httpClient;
    public ToDoItemsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<ToDoItemDetail>> CreateAsync(string description, string planId)
    {

        var response = await _httpClient.PostAsJsonAsync("/api/v2/todos", new
        {
            PlanId = planId,
            Description = description
        });

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ToDoItemDetail>>();
            return result!;
        }

        var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
        throw new ApiException(errorResponse!, response.StatusCode);

    }

    public async Task<ApiResponse<ToDoItemDetail>> EditAsync(string id, string description, string planId)
    {
        var response = await _httpClient.PutAsJsonAsync("/api/v2/todos", new
        {
            PlanId = planId,
            Description = description,
            Id = id
        });

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ToDoItemDetail>>();
            return result!;
        }

        var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
        throw new ApiException(errorResponse!, response.StatusCode);
    }

    public async Task DeleteAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"/api/v2/todos/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
            throw new ApiException(errorResponse!, response.StatusCode);
        }
    }

    public async Task ToggleAsync(string id)
    {
        var response = await _httpClient.PutAsJsonAsync<object>($"/api/v2/todos/{id}", null);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
            throw new ApiException(errorResponse!, response.StatusCode);
        }
    }
}
