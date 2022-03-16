using LostArkTools.Misc;

namespace LostArkTools.Models;

public class Character
{
    public string Name { get; set; }
    public List<ChecklistItem> Dailies { get; set; }
    public List<ChecklistItem> Weeklies { get; set; }

    public static Character Default => new()
    {
        Name = "New Character",
        Dailies = new List<ChecklistItem>(ChecklistDefaults.CharacterDaily),
        Weeklies = new List<ChecklistItem>(ChecklistDefaults.CharacterWeekly)
    };
}