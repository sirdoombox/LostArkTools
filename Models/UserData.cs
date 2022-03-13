using System;
using LostArkChecklist.Misc;

namespace LostArkChecklist.Models;

public class UserData
{
    public DateTime LastOpened { get; set; } = DateTime.UtcNow;
    public string LastCharacterOpened { get; set; } = string.Empty;
    public List<ChecklistItem> RosterDailies { get; set; } = new();
    public List<ChecklistItem> RosterWeeklies { get; set; } = new();
    public List<CharacterData> Characters { get; set; } = new();

    public static UserData Default => new()
    {
        LastOpened = Time.ServerTime,
        LastCharacterOpened = string.Empty,
        RosterDailies = new List<ChecklistItem>(ChecklistDefaults.RosterDaily),
        RosterWeeklies = new List<ChecklistItem>(ChecklistDefaults.RosterWeekly),
        Characters = new List<CharacterData> { CharacterData.Default }
    };
}