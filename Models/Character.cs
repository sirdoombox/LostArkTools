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
        Dailies = new List<ChecklistItem>(),
        Weeklies = new List<ChecklistItem>()
    };

    public void AddDailies(IEnumerable<ChecklistItem> dailies)
    {
        foreach(var daily in dailies)
            Dailies.Add(new ChecklistItem(daily.Title, daily.Note));
    }
    
    public void AddWeeklies(IEnumerable<ChecklistItem> weeklies)
    {
        foreach(var weekly in weeklies)
            Weeklies.Add(new ChecklistItem(weekly.Title, weekly.Note));
    }
}