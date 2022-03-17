using System.Linq;
using LostArkTools.Models;

namespace LostArkTools.Features.ServerStatus;

public class RegionViewModel : PropertyChangedBase
{
    public BindableCollection<ServerViewModel> Servers { get; } = new();
    public bool IsRegionOffline { get; }
    public string Name { get; }
    
    public RegionViewModel(RegionStatus regionStatus)
    {
        Name = regionStatus.Name;
        IsRegionOffline = regionStatus.IsRegionOffline;
        if (IsRegionOffline) return;
        Servers.AddRange(regionStatus.Servers.OrderBy(x => x.Name).Select(x => new ServerViewModel(x)));
    }
}