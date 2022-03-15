using System;
using System.IO;
using LostArkTools.Models;
using Newtonsoft.Json;

namespace LostArkTools.Services;

public class UserDataService
{
    private readonly string _dataDir;
    private readonly string _dataPath;
    private UserData? _currData;

    public UserDataService()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        _dataDir = Path.Combine(appData, "LostArkTools");
        _dataPath = Path.Combine(_dataDir, "UserData.dat");
    }
    
    public void SaveUserData()
    {
        Directory.CreateDirectory(_dataDir);
        File.WriteAllText(_dataPath, JsonConvert.SerializeObject(_currData));
    }

    public void LoadUserData()
    {
        if (!File.Exists(_dataPath))
        {
            _currData = UserData.Default;
            return;
        }
        var rawDat = File.ReadAllText(_dataPath);
        var dat = JsonConvert.DeserializeObject<UserData>(rawDat);
        _currData = dat;
    }

    public void SetLastOpened()
    {
        _currData.LastOpened = DateTime.UtcNow;
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