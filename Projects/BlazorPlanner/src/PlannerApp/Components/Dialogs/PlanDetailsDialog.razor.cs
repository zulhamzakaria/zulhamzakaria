using Microsoft.AspNetCore.Components;
using MudBlazor;
using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;

namespace PlannerApp.Components;

public partial class PlanDetailsDialog
{
    [CascadingParameter]
    MudDialogInstance? MudDialog { get; set; }

    [Inject]
    IPlansService? PlansService { get; set; }

    [Parameter]
    public string? PlanId { get; set; }

    private PlanDetail? _planDetail;
    private bool _isBusy;
    private string? _errorMessage = string.Empty;
    private void Close()
    {
        MudDialog!.Cancel();
    }

    protected override void OnParametersSet()
    {
        if (PlanId is null)
            throw new ArgumentNullException(nameof(PlanId));

        base.OnParametersSet();
    }

    protected override async Task OnInitializedAsync()
    {
        await FetchPlanAsync();
    }
    private async Task FetchPlanAsync()
    {
        _isBusy = true;
        try
        {
            var result = await PlansService!.GetByIdAsync(PlanId!);
            _planDetail = result.Value;
            StateHasChanged();
        }
        catch (ApiException ex)
        {

        }
        catch (Exception ex)
        {

            throw;
        }
        _isBusy = false;
    }

    private void OnToDoItemAddedCallback(ToDoItemDetail toDoItemDetail)
    {
        Console.WriteLine(toDoItemDetail.Id);
    }
}