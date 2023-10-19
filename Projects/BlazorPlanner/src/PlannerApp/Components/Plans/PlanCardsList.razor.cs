using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;

namespace PlannerApp.Components;

public partial class PlanCardsList
{
    private int _pageNumber = 1;
    private int _pageSize = 10;
    private string _query = String.Empty;

    [Inject]
    public NavigationManager? NavigationManager { get; set; }

    [Parameter]
    public EventCallback<PlanSummary> OnEditClicked { get; set; }

    private bool _isBusy { get; set; }
    [Parameter]
    // This calls the GetPlansAsync(string query = "", int pageNumber = 1, int pageSize = 10)
    // inside the PlansList.razor.cs
    public Func<string, int, int, Task<PagedList<PlanSummary>>>? FetchPlans { get; set; }

    private PagedList<PlanSummary>? _planSummaries = new();
    protected async override Task OnInitializedAsync()
    {
        await GetPlansAsync();
    }

    private async Task GetPlansAsync(int pageNumber = 1)
    {
        _pageNumber = pageNumber;
        _isBusy = true;
        _planSummaries = await FetchPlans!.Invoke(_query, _pageNumber, _pageSize);
        _isBusy = false;
    }

    //private void EditPlan(PlanSummary planSummary)
    //{
    //    NavigationManager?.NavigateTo($"/plans/form/{planSummary.Id}");
    //}
}