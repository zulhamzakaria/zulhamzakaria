using AKSoftware.Localization.MultiLanguages;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PlannerApp.Shared;

public partial class LanguageSwitcher
{
    [Inject]
    public ILanguageContainerService? LanguageContainerService { get; set; }

    [Inject]
    public ILocalStorageService? LocalStorageService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if(await LocalStorageService.ContainKeyAsync("language"))
        {
            string culture = await LocalStorageService.GetItemAsStringAsync("language");
            LanguageContainerService!.SetLanguage(CultureInfo.GetCultureInfo(culture));
        }
    }

    private async Task ChangeLanguageAsync(string culture)
    {
        LanguageContainerService!.SetLanguage(CultureInfo.GetCultureInfo(culture));
        await LocalStorageService!.SetItemAsStringAsync("language", culture);
    }
}