using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace PlannerApp.Pages.Plans;

public partial class Plans
{
    private List<BreadcrumbItem>? _breadCrumbItems = new()
    {
        new BreadcrumbItem("Home", "/index"),
        // disabled since we're currently in that page
        new BreadcrumbItem("Plans", "/plans", true)
    };
}