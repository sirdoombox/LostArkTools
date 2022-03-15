using MahApps.Metro.IconPacks;

namespace LostArkTools.Features.Shared;

public abstract class FeatureScreenBase : Screen
{
    private PackIconBoxIconsKind _displayIcon;
    public PackIconBoxIconsKind DisplayIcon
    {
        get => _displayIcon;
        set => SetAndNotify(ref _displayIcon, value);
    }

    public int Priority { get; }

    public FeatureScreenBase(string name, PackIconBoxIconsKind icon, int priority)
    {
        DisplayName = name;
        DisplayIcon = icon;
        Priority = priority;
    }
}