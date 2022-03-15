using LostArkTools.Services;

namespace LostArkTools.Features.Checklist.RosterChecklist;

public class RosterChecklistViewModel : Screen
{
    public Shared.TaskCollectionViewModel Dailies { get; } = new();
    public Shared.TaskCollectionViewModel Weeklies { get; } = new();

    public RosterChecklistViewModel(UserDataService userDataService)
    {
        Dailies.Populate(userDataService.GetRosterDailies());
        Weeklies.Populate(userDataService.GetRosterWeeklies());
    }
    
    public void ResetDaily()
    {
        
    }

    public void ResetWeekly()
    {
        
    }
}