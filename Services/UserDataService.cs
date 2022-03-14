using System;
using System.IO;
using LostArkChecklist.Misc;
using LostArkChecklist.Models;
using Newtonsoft.Json;

namespace LostArkChecklist.Services;

public class UserDataService
{
    private readonly string _dataDir;
    private readonly string _dataPath;
    private UserData _currData;

    public UserDataService()
    {
        _currData = UserData.Default;
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        _dataDir = Path.Combine(appData, "LostArkChecklist");
        _dataPath = Path.Combine(_dataDir, "UserData.dat");
    }
    
    public void SaveUserData()
    {
        Directory.CreateDirectory(_dataDir);
        File.WriteAllText(_dataPath, JsonConvert.SerializeObject(_currData));
    }

    public void LoadUserData()
    {
        if (!File.Exists(_dataPath)) return;
        var rawDat = File.ReadAllText(_dataPath);
        _currData = JsonConvert.DeserializeObject<UserData>(rawDat) ?? UserData.Default;
    }

    public void SetLastOpened()
    {
        _currData.LastOpened = Time.ServerTime;
    }

    public IEnumerable<CharacterData> GetCharacters() => 
        _currData.Characters;

    public CharacterData AddCharacter()
    {
        var newChar = CharacterData.Default;
        _currData.Characters.Add(newChar);
        return newChar;
    }

    public void RemoveCharacter(CharacterData characterData) =>
        _currData.Characters.Remove(characterData);

    public IEnumerable<ChecklistItem> GetRosterDailies() =>
        _currData.RosterDailies;

    public IEnumerable<ChecklistItem> GetRosterWeeklies() =>
        _currData.RosterWeeklies;
}