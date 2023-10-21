using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;

namespace PlannerApp.Components;

public partial class ToDoItems
{
    [Parameter]
    public ToDoItemDetail? ToDoItem { get; set; }

    private bool _isChecked = true;

    private bool _isEditMode = false;

    private string? _description = ":: default text ::";
}