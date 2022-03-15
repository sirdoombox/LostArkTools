using MahApps.Metro.IconPacks;

namespace LostArkChecklist.Features.Shared;

public class FeatureScreenBase : Screen
{
    private PackIconBoxIconsKind _displayIcon;
    public PackIconBoxIconsKind DisplayIcon
    {
        get => _displayIcon;
        set => SetAndNotify(ref _displayIcon, value);
    }
}