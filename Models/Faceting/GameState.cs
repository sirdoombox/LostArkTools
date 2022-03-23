using LostArkTools.Misc;

namespace LostArkTools.Models.Faceting;

public class GameState
{
    public Chance CurrChance { get; set; }
    public byte NumSlots { get; set; }
    public List<bool>[] Rows { get; set; } = { new(), new(), new() };
}