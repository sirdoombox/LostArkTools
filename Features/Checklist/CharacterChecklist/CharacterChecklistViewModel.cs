using LostArkTools.Features.Checklist.Shared;
using LostArkTools.Models;

namespace LostArkTools.Features.Checklist.CharacterChecklist;

public class CharacterChecklistViewModel : Screen
{
    private string _characterName = string.Empty;
    public string CharacterName
    {
        get => _characterName;
        set
        {
            Character.Name = value;
            SetAndNotify(ref _characterName, value);
        }
    }

    public Shared.TaskCollectionViewModel Dailies { get; } = new();
    public Shared.TaskCollectionViewModel Weeklies { get; } = new();

    public CharacterData Character { get; }
    
    public CharacterChecklistViewModel(CharacterData character)
    {
        Character = character;
        Dailies.Populate(character.Dailies);
        Weeklies.Populate(character.Weeklies);
        CharacterName = character.Name;
    }

    public void AddWeeklyClicked()
    {
        var newTask = new ChecklistItem();
        Character.Weeklies.Add(newTask);
        Weeklies.Tasks.Add(new TaskViewModel(newTask));
    }

    public void AddDailyClicked()
    {
        var newTask = new ChecklistItem();
        Character.Dailies.Add(newTask);
        Dailies.Tasks.Add(new TaskViewModel(newTask));
    }

    public void DailyReset() => Dailies.Reset();
    
    public void WeeklyReset() => Weeklies.Reset();
}