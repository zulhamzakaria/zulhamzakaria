using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;

namespace PlannerApp.Components;

public partial class PlansList
{
    [Inject]
    public IPlansService? PlansService { get; set; }

    [Inject]
    public NavigationManager? NavigationManager { get; set; }

    [Inject]
    public IDialogService? DialogService { get; set; }

    private bool _isBusy = false;
    private string _errorMessage = string.Empty;
    private int _pageNumber = 1;
    private int _pageSize = 10;
    private int _totalPages = 10;
    private string query = string.Empty;

    private List<PlanSummary> _plans = new();
    private async Task<PagedList<PlanSummary>> GetPlansAsync(string query = "", int pageNumber = 1, int pageSize = 10)
    {
        _isBusy = true;
        try
        {
            var result = await PlansService!.GetPlansAsync(query, pageNumber, pageSize);
            _plans = result.Value!.Records!.ToList();
            _pageNumber = result.Value.Page;
            _pageSize = result.Value.PageSize;
            _totalPages = result.Value.TotalPages;
            return result.Value;
        }
        catch (ApiException ex)
        {
            _errorMessage = ex.ApiErrorResponse!.Message!;
        }
        catch (Exception ex)
        {
            _errorMessage = ex.Message;
        }
        finally
        {
            _isBusy = false;
        }
        return new PagedList<PlanSummary>();
    }

    private bool _isCardsViewEnabled = true;
    private void SetCardsView()
    {
        _isCardsViewEnabled = true;
    }
    private void SetTableView()
    {
        _isCardsViewEnabled = false;
    }

    private void EditPlan(PlanSummary planSummary)
    {
        NavigationManager?.NavigateTo($"/plans/form/{planSummary.Id}");
    }

    #region Delete
    private async Task DeletePlan(PlanSummary planSummary)
    {
        // pass parameters to the razor page dialog
        var parameters = new DialogParameters<ConfirmationDialog>();
        parameters.Add(x => x.ContentText, $"Do you really want to delete the {planSummary.Title} plan? ");
        parameters.Add(x => x.ButtonText, "Delete");
        parameters.Add(x => x.Color, Color.Error);

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        // for handling the result
        var dialog = await DialogService!.Show<ConfirmationDialog>("Delete", parameters, options).Result;

        if (!dialog.Canceled)
        {
            try
            {
                await PlansService!.DeleteAsync(planSummary.Id!);

                // send message for deleted plan
                MessagingCenter.Send(this, "plan_deleted", planSummary);
            }
            catch (ApiException ex)
            {

            }
            catch (Exception ex)
            {

                throw;
            }
            // delete confirmed
        }
    }
    #endregion region

    #region View
    private void ViewPlan(PlanSummary planSummary)
    {
        // pass parameters to the razor page dialog
        var parameters = new DialogParameters<ConfirmationDialog>
        {
            { "PlanId", planSummary.Id }
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

        // for handling the result
        var dialog = DialogService!.Show<PlanDetailsDialog>("Details", parameters, options);
    }
    #endregion

}

