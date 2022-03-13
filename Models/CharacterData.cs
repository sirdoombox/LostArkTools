using LostArkChecklist.Misc;

namespace LostArkChecklist.Models;

public class CharacterData
{
    public string Name { get; set; }
    public List<ChecklistItem> Dailies { get; set; }
    public List<ChecklistItem> Weeklies { get; set; }

    public static CharacterData Default => new()
    {
        Name = "New Character",
        Dailies = new List<ChecklistItem>(ChecklistDefaults.CharacterDaily),
        Weeklies = new List<ChecklistItem>(ChecklistDefaults.CharacterWeekly)
    };
}