using MudBlazor;

namespace PlannerApp.Pages.Plans;

public partial class CreateEditPlan
{
    private List<BreadcrumbItem> breadcrumbItems = new()
    {
        new BreadcrumbItem("Home", "/index"),
        new BreadcrumbItem("Plans", "/plans"),
        new BreadcrumbItem("Create", "/plans/form", true)
    };
}