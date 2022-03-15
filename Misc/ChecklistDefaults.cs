using LostArkTools.Models;

namespace LostArkTools.Misc;

public class ChecklistDefaults
{
    public static readonly IReadOnlyList<ChecklistItem> RosterDaily = new ChecklistItem[]
    {
        new("Rapport Songs", "5x per day (+1 with Aura)"),
        new("Rapport Emotes", "5x per day (+1 with Aura)"),
        new("Trade Skills", "Deplete Energy"),
        new("Adventure Island", "When Available."),
        new("World Boss", "When Available."),
        new("Chaos Gate", "When Available."),
        new("Anguished Isle", "1100+")
    };

    public static readonly IReadOnlyList<ChecklistItem> RosterWeekly = new ChecklistItem[]
    {
        new("Placeholder","Placeholder")
    };

    public static readonly IReadOnlyList<ChecklistItem> CharacterDaily = new ChecklistItem[]
    {
        new("Chaos Dungeon", "x2"),
        new("Guardians", "x2"),
        new("Una Dailies", "x3"),
        new("Guild Support", "Research + Donation")
    };

    public static readonly IReadOnlyList<ChecklistItem> CharacterWeekly = new ChecklistItem[]
    {
        new("Abyss Dungeons", string.Empty),
        new("Una Weeklies", "x3"),
        new("Merchant Ship Exchange", string.Empty),
        new("Bloodstone Exchange", string.Empty),
        new("Chaos Dungeon Exchange", string.Empty)
    };
}