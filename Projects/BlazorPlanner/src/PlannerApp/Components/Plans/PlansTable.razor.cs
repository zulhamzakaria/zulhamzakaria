using Microsoft.AspNetCore.Components;
using MudBlazor;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;

namespace PlannerApp.Components;

public partial class PlansTable
{
    [Inject]
    public IPlansService? PlansService { get; set; }
    [Parameter]
    public EventCallback<PlanSummary> OnViewClicked { get; set; }
    [Parameter]
    public EventCallback<PlanSummary> OnDeleteClicked { get; set; }
    [Parameter]
    public EventCallback<PlanSummary> OnEditClicked { get; set; }

    private string _searchQuery = string.Empty;
    private MudTable<PlanSummary>? _table;
    private async Task<TableData<PlanSummary>> ServerReloadAsync(TableState state)
    {
        var result = await PlansService!.GetPlansAsync(_searchQuery, state.Page, state.PageSize);
        return new TableData<PlanSummary>
        {
            Items = result!.Value!.Records,
            TotalItems = result.Value.ItemsCount
        };
    }

    private void OnSearch(string query)
    {
        // this method will call ServerReloadAsync and wont be performing search function
        _searchQuery = query;
        _table?.ReloadServerData();
    }
}