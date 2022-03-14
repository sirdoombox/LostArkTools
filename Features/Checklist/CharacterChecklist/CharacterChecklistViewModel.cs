using LostArkChecklist.Features.Checklist.Shared;
using LostArkChecklist.Models;

namespace LostArkChecklist.Features.Checklist.CharacterChecklist;

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

    public Shared.ChecklistCollectionViewModel Dailies { get; } = new();
    public Shared.ChecklistCollectionViewModel Weeklies { get; } = new();

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
        Weeklies.Tasks.Add(new ChecklistItemViewModel(newTask));
    }

    public void AddDailyClicked()
    {
        var newTask = new ChecklistItem();
        Character.Dailies.Add(newTask);
        Dailies.Tasks.Add(new ChecklistItemViewModel(newTask));
    }

    public void DailyReset() => Dailies.Reset();
    
    public void WeeklyReset() => Weeklies.Reset();
}