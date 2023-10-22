using AKSoftware.Localization.MultiLanguages;
using AKSoftware.Localization.MultiLanguages.Blazor;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System.Net.Http.Json;

namespace PlannerApp.Components;

public partial class LoginForm : ComponentBase
{
    [Inject]
    public HttpClient? HttpClient { get; set; }
    [Inject]
    public NavigationManager? NavigationManager { get; set; }
    [Inject]
    public AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
    [Inject]
    public ILocalStorageService? LocalStorageService { get; set; }

    [Inject]
    public ILanguageContainerService LanguageContainerService { get; set; }

    private LoginRequest _model = new LoginRequest();
    public bool _isBusy = false;
    private string _errorMessage = string.Empty;

    protected override void OnInitialized()
    {
        // tell the app to change the language
        LanguageContainerService.InitLocalizedComponent(this);
    }

    private async Task LoginUserAsync()
    {
        _isBusy = true;
        _errorMessage= string.Empty;

        var response = await HttpClient!.PostAsJsonAsync("/api/v2/auth/login", _model);
        if (response.IsSuccessStatusCode)
        {
            // use the geenric response class
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResult>>();
            await LocalStorageService!.SetItemAsStringAsync("access_token", result!.Value!.Token);
            await LocalStorageService!.SetItemAsync<DateTime>("expiry_date", result.Value.ExpiryDate);
            await AuthenticationStateProvider!.GetAuthenticationStateAsync();
            NavigationManager!.NavigateTo("/");
        }
        else
        {
            var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
            _errorMessage = errorResult!.Message!;
        }

        _isBusy=false;
    }

    private void RedirectToRegister() 
        => NavigationManager!.NavigateTo("/authentication/register"); 
}
