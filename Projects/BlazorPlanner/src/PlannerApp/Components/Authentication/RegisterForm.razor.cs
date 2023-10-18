using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System.Net.Http.Json;

namespace PlannerApp.Components;

public partial class RegisterForm
{
    [Inject]
    // use the service to handle the http requests
    public IAuthenticationService? AuthenticationService { get; set; }
    [Inject]
    public NavigationManager? NavigationManager { get; set; }

    private RegisterRequest _model = new();
    public bool _isBusy = false;
    private string _errorMessage = string.Empty;

    private async Task RegisterUserAsync()
    {
        _isBusy = true;
        _errorMessage = string.Empty;

        try
        {
            await AuthenticationService!.RegisterUserAsync(_model);
            NavigationManager!.NavigateTo("/authentication/login");
        }
        catch (ApiException ex)
        {
           _errorMessage = ex.ApiErrorResponse!.Message!;
        }
        catch(Exception ex)
        {
            _errorMessage = ex.Message;
        }
        finally
        {
            _isBusy = false;
        }
    }

    private void RedirectToLogin()
    {
        NavigationManager!.NavigateTo("/authentication/login");
    }
}
