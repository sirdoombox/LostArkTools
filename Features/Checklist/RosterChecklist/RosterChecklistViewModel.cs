using LostArkChecklist.Services;

namespace LostArkChecklist.Features.Checklist.RosterChecklist;

public class RosterChecklistViewModel : Screen
{
    public Shared.ChecklistCollectionViewModel Dailies { get; } = new();
    public Shared.ChecklistCollectionViewModel Weeklies { get; } = new();

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