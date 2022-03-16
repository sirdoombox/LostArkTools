using LostArkTools.Features.Checklist.Shared;
using LostArkTools.Models;
using LostArkTools.Services;

namespace LostArkTools.Features.Checklist.RosterChecklist;

public class RosterChecklistViewModel : Screen
{
    public TaskCollectionViewModel Dailies { get; } = new();
    public TaskCollectionViewModel Weeklies { get; } = new();

    private readonly List<ChecklistItem> _rosterDailies;
    private readonly List<ChecklistItem> _rosterWeeklies;
    
    public RosterChecklistViewModel(UserDataService userDataService)
    {
        _rosterDailies = userDataService.GetRosterDailies();
        _rosterWeeklies = userDataService.GetRosterWeeklies();
        Dailies.Populate(_rosterDailies);
        Weeklies.Populate(_rosterWeeklies);
    }
    
    public void AddDailyClicked()
    {
        var newTask = new ChecklistItem();
        _rosterDailies.Add(newTask);
        Dailies.AddNewTask(newTask);
    }
    
    public void AddWeeklyClicked()
    {
        var newTask = new ChecklistItem();
        _rosterWeeklies.Add(newTask);
        Weeklies.AddNewTask(newTask);
    }

    public void ResetDaily() => Dailies.Reset();

    public void ResetWeekly() => Weeklies.Reset();
}