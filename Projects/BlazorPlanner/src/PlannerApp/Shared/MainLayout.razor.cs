using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace PlannerApp.Shared;

public partial class MainLayout
{
    [Inject]
    public ILocalStorageService? LocalStorageService { get; set; }

    private string? _themeName = "light";

    MudTheme _currentTheme = new();

    MudTheme _darkTheme = new()
    {
        Palette = new PaletteDark()
        {
            AppbarBackground = "#0097FF",
            AppbarText = "#FFFFFF",
            Primary = "#007CD1",
            TextPrimary = "#FFFFFF",
            Background = "#E0F1FF",
            TextSecondary = "#0C1217",
            DrawerBackground = "#C5E5FF",
            DrawerText = "#0C1217",
            Surface = "#C5E5FF",
            ActionDefault = "#0C1217",
            ActionDisabled = "#2F678C",
            TextDisabled = "#676767",

        }
    };
    MudTheme _lightTheme = new()
    {
        Palette = new PaletteLight()
        {
            AppbarBackground = "#0097FF",
            AppbarText = "#FFFFFF",
            Primary = "#007CD1",
            TextPrimary = "#0C1217",
            Background = "#F4FDFF",
            TextSecondary = "#E2EEF6",
            DrawerBackground = "#004675",
            DrawerText = "#FFFFFF",
            Surface = "#E4FAFF",
            ActionDefault = "#0C1217",
            ActionDisabled = "#2F678C",
            TextDisabled = "#B0B0B0",

        }
    };

    bool _drawerOpen = true;
    void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    protected async override Task OnInitializedAsync()
    {
        if (await LocalStorageService!.ContainKeyAsync("theme"))
        {
            _themeName = await LocalStorageService.GetItemAsStringAsync("theme");
        }
        else
            _themeName = "light";

        _currentTheme = _themeName == "light" ? _lightTheme : _darkTheme;
    }

    private async Task ChangeThemeAsync()
    {
        if(_themeName == "light")
        {
            _currentTheme = _darkTheme;
            _themeName = "dark";
        }
        else
        {
            _currentTheme = _lightTheme;
            _themeName = "light";
        }
        await LocalStorageService!.SetItemAsStringAsync("theme", _themeName);
    }
}