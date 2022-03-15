namespace LostArkChecklist.Models;

public class RegionStatus
{
    public string Name { get; set; }
    public List<ServerStatus> Servers { get; set; }
    public bool IsRegionOffline => Servers.Count <= 0;
}