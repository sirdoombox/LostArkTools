using LostArkTools.Models;
using LostArkTools.Services.Base;

namespace LostArkTools.Services;

public class AppSettingsService : LocalStorageServiceBase<AppSettings>
{
    public AppSettingsService() : base("AppSettings")
    {
    }

    protected override void OnLoadFailed()
    {
        Data = AppSettings.Default;
    }

    public (string theme, string accent) GetThemeAndAccent() => (Data.Theme, Data.Accent);

    public void SetThemeAndAccent(string theme, string accent)
    {
        Data.Theme = theme;
        Data.Accent = accent;
    }

    public string GetRegion() => Data.Region;
    public string SetRegion(string region) => Data.Region;
}