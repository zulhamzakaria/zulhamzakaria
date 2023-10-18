using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;

namespace PlannerApp.Components;

public partial class PlansList
{
    [Inject]
    public IPlansService? PlansService { get; set; }

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
}

