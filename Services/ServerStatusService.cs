using System.Threading.Tasks;
using AngleSharp;
using LostArkTools.Models;

namespace LostArkTools.Services;

public class ServerStatusService
{
    private static readonly string[] Regions =
    {
        "North America West",
        "North America East",
        "Europe Central",
        "Europe West",
        "South America"
    };

    private readonly IBrowsingContext _ctx;
    
    public ServerStatusService()
    {
        _ctx = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
    }

    public async Task<List<RegionStatus>> GetRegionStatuses()
    {
        var result = new List<RegionStatus>();
        var document = await _ctx.OpenAsync("https://www.playlostark.com/en-gb/support/server-status");
        var regions = document.QuerySelectorAll(".ags-ServerStatus-content-responses-response--centered");
        foreach (var region in regions)
        {
            var regionStatus = new RegionStatus();
            regionStatus.Name = Regions[int.Parse(region.GetAttribute("data-index"))];
            var servers = region.QuerySelectorAll(".ags-ServerStatus-content-responses-response-server");
            foreach (var server in servers)
            {
                var serverStatus = new ServerStatus();
                serverStatus.Name = server.QuerySelector(".ags-ServerStatus-content-responses-response-server-name")
                    .TextContent.Trim();
                if (server.QuerySelector(".ags-ServerStatus-content-responses-response-server-status--good") 
                    is not null)
                    serverStatus.Status = ServerStatus.Value.Good;
                else if (server.QuerySelector(".ags-ServerStatus-content-responses-response-server-status--maintenance")
                         is not null)
                    serverStatus.Status = ServerStatus.Value.Maintenance;
                else
                    serverStatus.Status = ServerStatus.Value.Busy;
                regionStatus.Servers.Add(serverStatus);
            }
            result.Add(regionStatus);
        }
        return result;
    }
}