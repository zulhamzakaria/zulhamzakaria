using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;

namespace PlannerApp.Client.Services.Interfaces;

public interface IAuthenticationService
{
    Task<ApiResponse<ApiErrorResponse>> RegisterUserAsync(RegisterRequest request);

}
