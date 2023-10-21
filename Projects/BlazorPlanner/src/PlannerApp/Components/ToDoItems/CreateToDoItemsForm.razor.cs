using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;

namespace PlannerApp.Components;

public partial class CreateToDoItemsForm
{
    [Inject]
    IToDoItemsService? ToDoItemsService { get; set; }
    [Parameter]
    public string? PlanId { get; set; }
    [Parameter]
    public EventCallback<ToDoItemDetail> OnToDoItemAdded { get; set; }

    private bool _isBusy = false;
    private string? _description { get; set; }
    private string? _errorMessage = string.Empty;


    private async Task AddToDoItemsAsync()
    {
        _errorMessage = string.Empty;
        try
        {
            if (string.IsNullOrWhiteSpace(_description))
            {
                _errorMessage = "Description required";
            }
            _isBusy = true;
            var result = await ToDoItemsService!.CreateAsync(_description, PlanId);
            _description = string.Empty;

            // to notify the parent about the added ToDo item
            await OnToDoItemAdded.InvokeAsync(result.Value);
        }
        catch (ApiException ex)
        {
            _errorMessage = ex.ApiErrorResponse!.Message;
        }
        catch (Exception ex)
        {
            _errorMessage = ex.Message;
        }
        _isBusy = false;
    }

}