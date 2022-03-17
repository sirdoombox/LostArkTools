using System;
using LostArkTools.Misc;

namespace LostArkTools.Models;

public class ChecklistData
{
    public DateTime LastOpened { get; set; } = DateTime.UtcNow;
    public string LastCharacterOpened { get; set; } = string.Empty;
    public List<ChecklistItem> RosterDailies { get; set; } = new();
    public List<ChecklistItem> RosterWeeklies { get; set; } = new();
    public List<Character> Characters { get; set; } = new();

    public static ChecklistData Default => new()
    {
        LastOpened = DateTime.UtcNow,
        LastCharacterOpened = string.Empty,
        RosterDailies = new List<ChecklistItem>(),
        RosterWeeklies = new List<ChecklistItem>(),
        Characters = new List<Character> { Character.Default }
    };
}