namespace LostArkChecklist.ViewModels;

public class CharacterChecklistViewModel : Screen
{
    private string _characterName = "New Character";
    public string CharacterName
    {
        get => _characterName;
        set => SetAndNotify(ref _characterName, value);
    }

    public TaskListViewModel Dailies { get; } = new();
    public TaskListViewModel Weeklies { get; } = new();

    public void DailyReset() => Dailies.Reset();
    
    public void WeeklyReset() => Dailies.Reset();
}