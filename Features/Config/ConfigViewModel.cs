using LostArkChecklist.Features.Shared;
using MahApps.Metro.IconPacks;

namespace LostArkChecklist.Features.Config;

public class ConfigViewModel : FeatureScreenBase
{
    public ConfigViewModel()
    {
        DisplayName = "Settings";
        DisplayIcon = PackIconBoxIconsKind.SolidCog;
    }
}