namespace LostArkChecklist.ViewModels;

public class RosterChecklistViewModel : Screen
{
    public BindableCollection<ChecklistItemViewModel> Dailies { get; } = new();
    public BindableCollection<ChecklistItemViewModel> Weeklies { get; } = new();

    public void ResetDaily()
    {
        foreach (var daily in Dailies)
            daily.IsComplete = false;
    }

    public void ResetWeekly()
    {
        foreach (var weekly in Weeklies)
            weekly.IsComplete = false;
    }
}