using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace PlannerApp.Components;

public partial class PlannerAppPage
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public List<BreadcrumbItem>? BreadcrumbItems { get; set; } = new();
}