using System;
using LostArkTools.Models;
using LostArkTools.Services.Base;

namespace LostArkTools.Services;

public class ChecklistDataService : LocalStorageServiceBase<ChecklistData>
{
    private readonly TimeService _ts;

    public ChecklistDataService(TimeService ts) : base("ChecklistData")
    {
        _ts = ts;
    }

    protected override void OnLoadFailed()
    {
        Data = ChecklistData.Default;
    }

    protected override void BeforeSave()
    {
        Data.LastOpened = _ts.ServerTime;
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

    public DateTime GetLastOpened() =>
        Data.LastOpened;

    public void SetLastOpenedCharacter(string activeItemCharacterName) =>
        Data.LastCharacterOpened = activeItemCharacterName;
}