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
}