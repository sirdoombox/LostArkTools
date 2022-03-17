using System.Linq;
using LostArkTools.Features.Shared;
using LostArkTools.Services;
using MahApps.Metro.IconPacks;

namespace LostArkTools.Features.ServerStatus;

public class ServerStatusViewModel : FeatureScreenBase
{
    public BindableCollection<RegionViewModel> Regions { get; } = new();

    private readonly ServerStatusService _statusService;
    
    
    
    public ServerStatusViewModel(ServerStatusService statusService) : base("Server Status", PackIconBoxIconsKind.SolidServer, 2)
    {
        _statusService = statusService;
    }

    protected override void OnActivate() => Refresh();

    public async void Refresh()
    {
        Regions.Clear();
        var res = await _statusService.GetRegionStatuses();
        Regions.AddRange(res.Select(x => new RegionViewModel(x)));
    }
}