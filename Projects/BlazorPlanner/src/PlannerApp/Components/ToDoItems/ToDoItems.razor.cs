using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;

namespace PlannerApp.Components;

public partial class ToDoItems
{
    [Parameter]
    public ToDoItemDetail? ToDoItem { get; set; }

    private bool _isChecked = true;

    private bool _isEditMode = false;

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
}