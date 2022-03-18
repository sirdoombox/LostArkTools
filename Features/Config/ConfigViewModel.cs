using System.Diagnostics;
using System.Linq;
using System.Windows;
using ControlzEx.Theming;
using LostArkTools.Extensions;
using LostArkTools.Features.Shared;
using LostArkTools.Services;
using MahApps.Metro.IconPacks;
using StyletIoC;

namespace LostArkTools.Features.Config;

public class ConfigViewModel : FeatureScreenBase
{
    public BindableCollection<string> Themes { get; set; } = new();
        
    public BindableCollection<string> Accents { get; set; } = new();

    public BindableCollection<string> Regions { get; set; } = new();

    public BindableCollection<ContributionViewModel> Contributions { get; } = new();

    private string _selectedTheme;
    public string SelectedTheme
    {
        get => _selectedTheme;
        set
        {
            SetAndNotify(ref _selectedTheme, value);
            UpdateTheme();
        }
    }

    private string _selectedAccent;
    public string SelectedAccent
    {
        get => _selectedAccent;
        set
        {
            SetAndNotify(ref _selectedAccent, value);
            UpdateTheme();
        }
    }
    
    private string _selectedRegion;
    public string SelectedRegion
    {
        get => _selectedRegion;
        set
        {
            SetAndNotify(ref _selectedRegion, value);
            UpdateRegion();
        }
    }
    
    private readonly AppSettingsService _settings;
    private readonly TimeService _timeService;
    
    public ConfigViewModel(IContainer container) : base("Settings", PackIconBoxIconsKind.SolidCog, int.MaxValue)
    {
        _settings = container.GetStorageService<AppSettingsService>();
        _timeService = container.Get<TimeService>();
        
        Accents.AddRange(ThemeManager.Current.ColorSchemes);
        Themes.AddRange(ThemeManager.Current.BaseColors);
        var (theme, accent) = _settings.GetThemeAndAccent();
        SelectedAccent = accent;
        SelectedTheme = theme;
        
        Regions.AddRange(_timeService.GetRegions());
        SelectedRegion = _settings.GetRegion();
        
        Contributions.AddRange(ContributionViewModel.Libraries);
    }

    public void OpenUrl(string url)
    {
        Process.Start(new ProcessStartInfo(url) {UseShellExecute = true});
    }

    private void UpdateTheme()
    {
        if (SelectedTheme is null) return;
        ThemeManager.Current.ChangeTheme(Application.Current, SelectedTheme, SelectedAccent);
        _settings.SetThemeAndAccent(SelectedTheme, SelectedAccent);
    }
    
    private void UpdateRegion()
    {
        _timeService.SetTimezone(SelectedRegion);
        _settings.SetRegion(SelectedRegion);
    }
}