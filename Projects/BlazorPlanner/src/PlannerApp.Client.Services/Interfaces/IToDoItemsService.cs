using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;

namespace PlannerApp.Client.Services.Interfaces;

public interface IToDoItemsService
{
    Task<ApiResponse<ToDoItemDetail>> CreateAsync(string description, string planId);
    Task<ApiResponse<ToDoItemDetail>> EditAsync(string id, string newDescription, string planId);
    Task ToggleAsync(string id);
    Task DeleteAsync(string id);
}

