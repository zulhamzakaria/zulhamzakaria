using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;

namespace PlannerApp.Components;

public partial class PlanCard
{
    [Parameter]
    public PlanSummary?PlanSummary { get; set; }
    [Parameter]
    public bool IsBusy { get; set; }
    [Parameter]
    public EventCallback<PlanSummary> OnViewClicked { get; set; }
    [Parameter]
    public EventCallback<PlanSummary> OnDeleteClicked { get; set; }
    [Parameter]
    public EventCallback<PlanSummary> OnEditClicked { get; set; }
}