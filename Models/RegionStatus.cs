namespace LostArkTools.Models;

public class RegionStatus
{
    public string Name { get; set; } = string.Empty;
    public List<ServerStatus> Servers { get; set; } = new();
    public bool IsRegionOffline => Servers.Count <= 0;
}