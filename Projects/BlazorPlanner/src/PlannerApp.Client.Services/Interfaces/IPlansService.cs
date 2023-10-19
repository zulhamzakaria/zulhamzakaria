using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;

namespace PlannerApp.Client.Services.Interfaces;

public interface IPlansService
{
    Task<ApiResponse<PagedList<PlanSummary>>> GetPlansAsync(string? query = null, int intPageNo = 1, int pageSize = 10);
    Task<ApiResponse<PlanDetail>> CreateAsync(PlanDetail planDetail, FormFile formFile);
    Task<ApiResponse<PlanDetail>> EditAsync(PlanDetail planDetail, FormFile formFile);
    Task<ApiResponse<PlanDetail>> GetByIdAsync(string id);
    Task DeleteAsync(string id);
}

