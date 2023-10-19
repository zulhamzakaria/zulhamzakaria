using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Xml.Schema;

namespace PlannerApp.Client.Services;

public class HttpPlansService : IPlansService
{
    private readonly HttpClient _httpClient;
    public HttpPlansService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<PlanDetail>> CreateAsync(PlanDetail planDetail, FormFile formFile)
    {
        var form = PreparePlanForm(planDetail, formFile, false);
        var response = await _httpClient.PostAsync("/api/v2/plans", form);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<PlanDetail>>();
            return result!;
        }

        var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
        throw new ApiException(errorResponse!, response.StatusCode);

    }

    public async Task<ApiResponse<PlanDetail>> EditAsync(PlanDetail planDetail, FormFile formFile)
    {
        var form = PreparePlanForm(planDetail, formFile, true);
        var response = await _httpClient.PutAsync("/api/v2/plans", form);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<PlanDetail>>();
            return result!;
        }

        var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
        throw new ApiException(errorResponse!, response.StatusCode);
    }

    public async Task<ApiResponse<PlanDetail>> GetByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"/api/v2/plans/{id}");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<PlanDetail>>();
            return result!;
        }

        var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
        throw new ApiException(errorResponse!, response.StatusCode);

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

    private HttpContent PreparePlanForm(PlanDetail planDetail, FormFile coverFile, bool isUpdate)
    {
        var form = new MultipartFormDataContent();
        form?.Add(new StringContent(planDetail!.Title!), nameof(PlanDetail.Title));
        if (!string.IsNullOrEmpty(planDetail.Description))
            form?.Add(new StringContent(planDetail.Description), nameof(PlanDetail.Description));
        if(isUpdate)
            form?.Add(new StringContent(planDetail!.Id!), nameof(PlanDetail.Id));
        if(coverFile is not null)
        {
            form?.Add(new StreamContent(coverFile!.FileStream!), nameof(PlanDetail.CoverFile));
        }

        return form!;
    }
}
