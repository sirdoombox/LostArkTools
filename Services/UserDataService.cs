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
    public UserData CurrentData { get; private set; }

    public UserDataService()
    {
        CurrentData = UserData.Default;
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        _dataDir = Path.Combine(appData, "LostArkChecklist");
        _dataPath = Path.Combine(_dataDir, "UserData.dat");
    }
    
    public void SaveUserData()
    {
        Directory.CreateDirectory(_dataDir);
        File.WriteAllText(_dataPath, JsonConvert.SerializeObject(CurrentData));
    }

    public void LoadUserData()
    {
        if (!File.Exists(_dataPath)) return;
        var rawDat = File.ReadAllText(_dataPath);
        CurrentData = JsonConvert.DeserializeObject<UserData>(rawDat) ?? UserData.Default;
    }

    public void SetLastOpened()
    {
        CurrentData.LastOpened = Time.ServerTime;
    }
}