using System;
using LostArkTools.Models;
using LostArkTools.Services.Base;

namespace LostArkTools.Services;

public class ChecklistDataService : LocalStorageServiceBase<ChecklistData>
{
    public ChecklistDataService() : base("ChecklistData")
    {
    }

    protected override void OnLoadFailed()
    {
        Data = ChecklistData.Default;
    }

    public void SetLastOpened()
    {
        Data.LastOpened = DateTime.UtcNow;
    }

    public IEnumerable<Character> GetCharacters() =>
        Data.Characters;

    public Character AddCharacter()
    {
        var newChar = Character.Default;
        Data.Characters.Add(newChar);
        return newChar;
    }

    public void RemoveCharacter(Character character) =>
        Data.Characters.Remove(character);

    public List<ChecklistItem> GetRosterDailies() =>
        Data.RosterDailies;

    public List<ChecklistItem> GetRosterWeeklies() =>
        Data.RosterWeeklies;
}