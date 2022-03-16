using LostArkTools.Features.Checklist.Shared;
using LostArkTools.Models;
using LostArkTools.Services;

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

    public TaskCollectionViewModel Dailies { get; } = new();
    public TaskCollectionViewModel Weeklies { get; } = new();

    public CharacterData Character { get; }

    public CharacterChecklistViewModel(CharacterData character)
    {
        Character = character;
        Dailies.Populate(character.Dailies);
        Weeklies.Populate(character.Weeklies);
        CharacterName = character.Name;
    }

    public void AddDailyClicked()
    {
        var newTask = new ChecklistItem();
        Character.Dailies.Add(newTask);
        Dailies.AddNewTask(newTask);
    }

    public void AddWeeklyClicked()
    {
        var newTask = new ChecklistItem();
        Character.Weeklies.Add(newTask);
        Weeklies.AddNewTask(newTask);
    }

    public void DailyReset() => Dailies.Reset();

    public void WeeklyReset() => Weeklies.Reset();
}