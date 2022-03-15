namespace LostArkTools.Models;

public class ServerStatus
{
    public enum Value { Good, Busy, Maintenance }
    
    public string Name { get; set; }
    public Value Status { get; set; }
}