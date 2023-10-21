using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models;
using PlannerApp.Client.Services.Interfaces;
using System.Diagnostics.Tracing;

namespace PlannerApp.Components;

public partial class ToDoItems
{

    [Inject]
    IToDoItemsService? ToDoItemsService { get; set; }

    [Parameter]
    public ToDoItemDetail? ToDoItem { get; set; }

    [Parameter]
    public EventCallback<ToDoItemDetail> OnItemDeleted { get; set; }

    [Parameter]
    public EventCallback<ToDoItemDetail> OnItemEdited { get; set; }

    private bool _isChecked = true;
    private bool _isBusy = false;
    private bool _isEditMode = false;
    private string? _errorMessage = string.Empty;
    private string? _description = string.Empty;
    private void ToggleEditMode(bool isCancel)
    {
        if (_isEditMode)
        {
            _isEditMode = false;
            _description = isCancel ? ToDoItem?.Description : _description;
            return;
        }
        _isEditMode = true;
        _description = ToDoItem!.Description;
    }

    private async Task RemoveToDoItemAsync()
    {
        try
        {
            _isBusy = true;
            await ToDoItemsService!.DeleteAsync(ToDoItem.Id);

            // to notify the parent about the added ToDo item
            await OnItemDeleted.InvokeAsync(ToDoItem);
        }
        catch (ApiException ex)
        {
            // TO DO
        }
        catch (Exception ex)
        {
            // TO DO
        }
        _isBusy = false;
    }
    private async Task EditToDoItemAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(_description))
            {
                _errorMessage = "Description required";
                return;
            }

            _isBusy = true;
            var result = await ToDoItemsService!.EditAsync(ToDoItem.Id, _description, ToDoItem.PlanId);
            ToggleEditMode(false);

            // to notify the parent about the added ToDo item
            await OnItemEdited.InvokeAsync(result.Value);
        }
        catch (ApiException ex)
        {
            // TO DO
        }
        catch (Exception ex)
        {
            // TO DO
        }
        _isBusy = false;
    }
}