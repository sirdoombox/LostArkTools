using LostArkTools.Models;

namespace LostArkTools.Features.ServerStatus;

public class ServerViewModel : PropertyChangedBase
{
    public string Name { get; }
    public Models.ServerStatus.Value Value { get; }
    
    public ServerViewModel(Models.ServerStatus serverStatus)
    {
        Value = serverStatus.Status;
        Name = serverStatus.Name;
    }
}