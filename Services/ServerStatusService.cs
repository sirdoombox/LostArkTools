using System.Threading.Tasks;
using AngleSharp;
using LostArkChecklist.Models;

namespace LostArkChecklist.Services;

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

    public async Task<List<RegionStatus>> GetRegionStatuses()
    {
        var result = new List<RegionStatus>();
        var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
        var document = await context.OpenAsync("https://www.playlostark.com/en-gb/support/server-status");
        return result;
    }
}