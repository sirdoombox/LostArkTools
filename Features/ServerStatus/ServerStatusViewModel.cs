using LostArkChecklist.Features.Shared;
using MahApps.Metro.IconPacks;

namespace LostArkChecklist.Features.ServerStatus;

public class ServerStatusViewModel : FeatureScreenBase
{
    public ServerStatusViewModel()
    {
        DisplayName = "Server Status";
        DisplayIcon = PackIconBoxIconsKind.SolidServer;
    }
}